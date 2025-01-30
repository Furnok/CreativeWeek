using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class S_ProfilStateManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float _timeDisplayNewState;

    [Header("References")]
    [SerializeField] Image ImageProfil;

    [Header("RSE")]
    [SerializeField] RSE_ProfilStateChange _rseProfilStateChange;

    [Header("RSO")]
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;

    //[Header("SSO")]

    private void Start()
    {
        ImageProfil.sprite = _rsoCurrentProfile.Value.ListProfilStates.FirstOrDefault(x => x.State == ProfilState.Neutral).Sprite;

        _rseProfilStateChange.action += UpdateState;
    }

    private void OnDestroy()
    {
        _rseProfilStateChange.action -= UpdateState;

    }

    void UpdateState(ProfilState state)
    {
        StartCoroutine(DisplayStateProfil(state));
    }

    IEnumerator DisplayStateProfil(ProfilState state)
    {
        ImageProfil.sprite = _rsoCurrentProfile.Value.ListProfilStates.FirstOrDefault(x => x.State == state).Sprite;

        yield return new WaitForSeconds(_timeDisplayNewState);

        ImageProfil.sprite = _rsoCurrentProfile.Value.ListProfilStates.FirstOrDefault(x => x.State == ProfilState.Neutral).Sprite;


    }
}