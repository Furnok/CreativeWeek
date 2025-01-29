using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_UIDate : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] Slider charmSlider;
    [SerializeField] TextMeshProUGUI charmText;
    [Header("RSE")]
    [SerializeField] RSE_UpdateUICharm RSE_UpdateUICharm;
    [Header("RSO")]
    [SerializeField] RSO_Charm RSO_Charm;
    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_UpdateUICharm.action += UpdateUICharm;
    }
    private void OnDisable()
    {
        RSE_UpdateUICharm.action -= UpdateUICharm;
    }
    private void Start()
    {
        charmText.text = $"{RSO_Charm.Value}%";
        charmSlider.value = RSO_Charm.Value;
    }
    private void UpdateUICharm()
    {
        charmText.text = $"{RSO_Charm.Value}%";
        charmSlider.value = RSO_Charm.Value;
    }
}