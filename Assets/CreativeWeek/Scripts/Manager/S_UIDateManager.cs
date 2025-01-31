using UnityEngine;

public class S_UIDateManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [Header("RSE")]
    [SerializeField] RSE_CallWinDate RSE_CallWinDate;
    [SerializeField] RSE_CallLoseDate RSE_CallLoseDate;
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;

    //[Header("RSO")]


    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallWinDate.action += Win;
        RSE_CallLoseDate.action += Lose;
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;

    }
    private void OnDisable()
    {
        RSE_CallWinDate.action -= Win;
        RSE_CallLoseDate.action -= Lose;
        callPause.action -= ShowPause;
        unCallPause.action -= UnShowPause;

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