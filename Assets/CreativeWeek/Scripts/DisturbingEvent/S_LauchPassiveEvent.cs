using System.Collections;
using UnityEngine;

public class S_LauchPassiveEvent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int timeMin;
    [SerializeField] int timeMax;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_ChoosePassiveEvent RSE_ChoosePassiveEvent;
    [SerializeField] RSE_OnDateStepChange RSE_OnDateStepChange;

    [Header("RSO")]
    [SerializeField] RSO_CurrentDateStep RSO_CurrentDateStep;

    //[Header("SSO")]
    private Coroutine coroutineLaunchEventPassive;
    private void OnEnable()
    {
        RSE_OnDateStepChange.action += CheckDateStep;
    }
    private void OnDisable()
    {
        RSE_OnDateStepChange.action -= CheckDateStep;
    }
    private void CheckDateStep()
    {
        if (RSO_CurrentDateStep.Value == DateStep.Starter)
        {
            coroutineLaunchEventPassive = StartCoroutine(LaunchEvent());
        }
        else if (RSO_CurrentDateStep.Value == DateStep.Bill)
        {
            StopCoroutine(coroutineLaunchEventPassive);
        }
    }
    IEnumerator LaunchEvent()
    {
        float randomTime = Random.Range(timeMin, timeMax);
        yield return new WaitForSeconds(randomTime);
        RSE_ChoosePassiveEvent?.RaiseEvent();
        coroutineLaunchEventPassive = StartCoroutine(LaunchEvent());
    }
}