using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UIPrepaManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelFond;
    [SerializeField] private GameObject panelPhone;
    [SerializeField] private GameObject panelTel;
    [SerializeField] private GameObject panelMatch;
    [SerializeField] private GameObject panelProfil;
    [SerializeField] private GameObject panelButtonPhone;

    [Header("RSE")]
    [SerializeField] private RSE_StartTimerPrepa rseStartTimerPrepa;
    [SerializeField] private RSE_EndTimer rseEndTimer;
    [SerializeField] private RSE_CallPause callPause;
    [SerializeField] private RSE_UnCallPause unCallPause;
    [SerializeField] private RSE_PlaySound rsePlaySoundNotif;
    [SerializeField] private RSE_PlaySound rsePlaySoundMatch;
    [SerializeField] private RSE_PlaySound rsePlaySoundMusic;
    [SerializeField] private RSE_RemovePhone rseRemovePhone;

    private IEnumerator StartIntro()
    {
        panelFond.SetActive(true);

        rsePlaySoundNotif.RaiseEvent();

        yield return new WaitForSeconds(2f);

        panelPhone.SetActive(true);
        panelTel.SetActive(true);

        yield return new WaitForSeconds(2f);

        rsePlaySoundMatch.RaiseEvent();

        panelMatch.SetActive(true);

        yield return new WaitForSeconds(2f);

        panelProfil.SetActive(true);
        panelButtonPhone.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(StartIntro());
    }

    private void OnEnable()
    {
        rseEndTimer.action += LauchDate;
        callPause.action += ShowPause;
        unCallPause.action += UnShowPause;
        rseRemovePhone.action += LauchPrepa;
    }

    private void OnDisable()
    {
        rseEndTimer.action -= LauchDate;
        callPause.action -= ShowPause;
        unCallPause.action -= UnShowPause;
        rseRemovePhone.action -= LauchPrepa;

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

    private void LauchPrepa()
    {
        panelFond.SetActive(false);
        panelPhone.SetActive(false);
        panelTel.SetActive(false);
        panelMatch.SetActive(false);
        panelProfil.SetActive(false);
        panelButtonPhone.SetActive(false);

        rsePlaySoundMusic.RaiseEvent();

        rseStartTimerPrepa?.RaiseEvent();
    }
}