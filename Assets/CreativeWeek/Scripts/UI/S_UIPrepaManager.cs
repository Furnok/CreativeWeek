using UnityEngine;
using UnityEngine.SceneManagement;

public class S_UIPrepaManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_StartTimerPrepa rseStartTimerPrepa;
    [SerializeField] private RSE_EndTimer rseEndTimer;

    private void Start()
    {
        rseStartTimerPrepa?.RaiseEvent();
    }

    private void OnEnable()
    {
        rseEndTimer.action += LauchDate;
    }

    private void OnDisable()
    {
        rseEndTimer.action -= LauchDate;
    }

    private void LauchDate()
    {
        SceneManager.LoadScene("Scene_Date");
    }
}