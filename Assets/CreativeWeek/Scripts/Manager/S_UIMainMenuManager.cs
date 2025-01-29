using UnityEngine;

public class S_UIMainMenuManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] GameObject creditPanel;

    [Header("RSE")]
    [SerializeField] RSE_CallOpenCredit RSE_CallOpenCredit;
    [SerializeField] RSE_CallCloseCredit RSE_CallCloseCredit;

    //[Header("RSO")]

    //[Header("SSO")]
    private void OnEnable()
    {
        RSE_CallOpenCredit.action += OpenCredit;
        RSE_CallCloseCredit.action += CloseCredit;
    }
    private void OnDisable()
    {
        RSE_CallOpenCredit.action -= OpenCredit;
        RSE_CallCloseCredit.action -= CloseCredit;
    }
    private void OpenCredit()
    {
        creditPanel.SetActive(true);
    }

    private void CloseCredit()
    {
        creditPanel.SetActive(false);
    }
}