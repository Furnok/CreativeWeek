using System.Collections;
using TMPro;
using UnityEngine;

public class S_AmbulanceEvent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float delayAmbulanceSound;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_IsTextDisturbed RSE_IsTextDisturbed;
    [SerializeField] RSE_CallAmbulanceEvent RSE_CallAmbulanceEvent;
    [SerializeField] RSE_PlaySound RSE_PlaySoundAmbulance;

    //[Header("RSO")]

    //[Header("SSO")]
    private void OnEnable()
    {
        RSE_CallAmbulanceEvent.action += AmbulanceEvent;
    }
    private void OnDisable()
    {
        RSE_CallAmbulanceEvent.action -= AmbulanceEvent;
    }
    private void AmbulanceEvent()
    {
        RSE_PlaySoundAmbulance?.RaiseEvent();
        StartCoroutine(DisplayDisturbedText(delayAmbulanceSound));
    }
    

    IEnumerator DisplayDisturbedText(float delay)
    {
        RSE_IsTextDisturbed?.RaiseEvent(true);
        yield return new WaitForSeconds(delay);
        RSE_IsTextDisturbed?.RaiseEvent(false);
    }
}