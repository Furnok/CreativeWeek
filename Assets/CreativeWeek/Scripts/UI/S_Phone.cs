using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_Phone : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] private Image imageProfil;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAge;
    [SerializeField] private TextMeshProUGUI textType;
    [SerializeField] private TextMeshProUGUI textDescription;

    //[Header("RSE")]

    [Header("RSO")]
    [SerializeField] private RSO_CurrentProfile rsoCurrentProfile;

    //[Header("SSO")]

    private void OnEnable()
    {
        imageProfil.sprite = rsoCurrentProfile.Value.ListProfilStates[0].Sprite;
        textName.text = rsoCurrentProfile.Value.Name;
        textAge.text = rsoCurrentProfile.Value.Age.ToString() + " years";
        textType.text = rsoCurrentProfile.Value.ProfilType.ToString();
        textDescription.text = rsoCurrentProfile.Value.Description;
    }
}