using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class S_UIDateManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] Slider charmSlider;
    [SerializeField] TextMeshProUGUI charmText;

    [Header("RSE")]
    [SerializeField] RSE_CallWinDate RSE_CallWinDate;
    [SerializeField] RSE_CallLoseDate RSE_CallLoseDate;
    [SerializeField] RSE_UpdateUICharm RSE_UpdateUICharm;
    [Header("RSO")]
    [SerializeField] RSO_Charm RSO_Charm;

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallWinDate.action += Win;
        RSE_CallLoseDate.action += Lose;
        RSE_UpdateUICharm.action += UpdateUICharm;
    }
    private void OnDisable()
    {
        RSE_CallWinDate.action -= Win;
        RSE_CallLoseDate.action -= Lose;
        RSE_UpdateUICharm.action -= UpdateUICharm;
    }
    private void Start()
    {
        charmText.text = $"{RSO_Charm.Value}%";
        charmSlider.value = RSO_Charm.Value;
    }

    private void Win()
    {
        panelWin.SetActive(true);
    }

    private void Lose()
    {
        panelLose.SetActive(true);
    }

    private void UpdateUICharm()
    {
        charmText.text = $"{RSO_Charm.Value}%";
        charmSlider.value = RSO_Charm.Value;
    }
}