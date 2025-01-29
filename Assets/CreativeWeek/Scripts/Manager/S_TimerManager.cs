using UnityEngine;
using System.Collections;

public class S_TimerManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] private RSE_StartTimerPrepa rseStartTimerPrepa;
    [SerializeField] private RSE_UpdateTimer rseUpdateTimer;
    [SerializeField] private RSE_EndTimer rseEndTimer;

    [Header("RSO")]
    [SerializeField] private RSO_TimerPreparation rsoTimerPreparation;

    [Header("SSO")]
    [SerializeField] private SSO_TimerPreparation ssoTimerPreparation;

    private void Start()
    {
        rsoTimerPreparation.Value = ssoTimerPreparation.Value;
    }

    private void OnEnable()
    {
        rseStartTimerPrepa.action += StartTimer;
    }

    private void OnDisable()
    {
        rseStartTimerPrepa.action -= StartTimer;
    }

    private IEnumerator SliderTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < ssoTimerPreparation.Value)
        {
            elapsedTime += Time.deltaTime;

            rsoTimerPreparation.Value = Mathf.Lerp(ssoTimerPreparation.Value, 0f, elapsedTime / ssoTimerPreparation.Value);

            rseUpdateTimer?.RaiseEvent();

            yield return null;
        }

        rseEndTimer?.RaiseEvent();
    }

    private void StartTimer()
    {
        StartCoroutine(SliderTime());
    }
}