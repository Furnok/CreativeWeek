using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UIPrepaManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject panelPause;

    [Header("RSE")]
    [SerializeField] private RSE_StartTimerPrepa rseStartTimerPrepa;
    [SerializeField] private RSE_EndTimer rseEndTimer;
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;


    private void Start()
    {
        rseStartTimerPrepa?.RaiseEvent();
    }

    private void OnEnable()
    {
        rseEndTimer.action += LauchDate;
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;
    }

    private void OnDisable()
    {
        rseEndTimer.action -= LauchDate;
        callPause.action -= ShowPause;
        unCallPause.action -= UnShowPause;

        Time.timeScale = 1;
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

    private void LauchDate()
    {
        SceneManager.LoadScene("Scene_Date");
    }
}