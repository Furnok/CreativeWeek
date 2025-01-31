using UnityEngine;

public class S_UIDateManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] private GameObject panelFond;
    [SerializeField] private GameObject panelPhone;
    [SerializeField] private GameObject panelTel;
    [SerializeField] private GameObject panelProfil;
    [SerializeField] private GameObject panelButtonPhone;
    [Header("RSE")]
    [SerializeField] RSE_CallWinDate RSE_CallWinDate;
    [SerializeField] RSE_CallLoseDate RSE_CallLoseDate;
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;
    [SerializeField] private RSE_EnterToilet rseEnterToilet;
    [SerializeField] private RSE_ExitToilet rseExitToilet;
    [SerializeField] private RSE_RemovePhone rseRemovePhone;

    //[Header("RSO")]


    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallWinDate.action += Win;
        RSE_CallLoseDate.action += Lose;
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;
        rseEnterToilet.action += ShowPhone;
        rseRemovePhone.action += UnShowPhone;
    }
    private void OnDisable()
    {
        RSE_CallWinDate.action -= Win;
        RSE_CallLoseDate.action -= Lose;
        callPause.action -= ShowPause;
        unCallPause.action -= UnShowPause;
        rseEnterToilet.action -= ShowPhone;
        rseRemovePhone.action -= UnShowPhone;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelPause.activeInHierarchy)
            {
                UnShowPause();
            }
            else
            {
                ShowPause();
            }
        }
    }

    private void ShowPhone()
    {
        panelFond.SetActive(true);
        panelPhone.SetActive(true);
        panelTel.SetActive(true);
        panelProfil.SetActive(true);
        panelFond.SetActive(true);
        panelButtonPhone.SetActive(true);
    }

    private void UnShowPhone()
    {
        panelFond.SetActive(false);
        panelPhone.SetActive(false);
        panelTel.SetActive(false);
        panelProfil.SetActive(false);
        panelFond.SetActive(false);
        panelButtonPhone.SetActive(false);
    }


    private void ShowPause()
    {
        panelPause.SetActive(true);

        Time.timeScale = 0;
    }

    private void UnShowPause()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        panelPause.SetActive(false);

        Time.timeScale = 1;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void Win()
    {
        panelWin.SetActive(true);
    }
    private void Lose()
    {
        panelLose.SetActive(true);
    }

    
}