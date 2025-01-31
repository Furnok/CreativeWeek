using System.Collections;
using UnityEngine;

public class S_MariachiEvent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float delayMariachiSound;
    [Header("References")]
    [SerializeField] GameObject SpriteMariachi;

    [Header("RSE")]
    [SerializeField] RSE_IsTextDisturbed RSE_IsTextDisturbed;
    [SerializeField] RSE_CallMariachiEvent RSE_CallMariachiEvent;
    [SerializeField] RSE_PlaySound RSE_PlaySoundMariachi;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallMariachiEvent.action += MariachiEvent;
    }
    private void OnDisable()
    {
        RSE_CallMariachiEvent.action -= MariachiEvent;
    }
    private void MariachiEvent()
    {
        SpriteMariachi.SetActive(true);
        RSE_PlaySoundMariachi?.RaiseEvent();
        StartCoroutine(DisplayDisturbedText(delayMariachiSound));
    }
    IEnumerator DisplayDisturbedText(float delay)
    {
        RSE_IsTextDisturbed?.RaiseEvent(true);
        yield return new WaitForSeconds(delay);
        RSE_IsTextDisturbed?.RaiseEvent(false);
        SpriteMariachi.SetActive(false);
    }
}