using System.Collections;
using UnityEngine;

public class S_FriendEvent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitude;

    [Header("References")]
    [SerializeField] GameObject test;

    [Header("RSE")]
    [SerializeField] RSE_CallFriendEvent RSE_CallFriendEvent;
    [SerializeField] RSE_PlaySound RSE_PlaySoundFriend;

    //[Header("RSO")]

    //[Header("SSO")]
    private Vector3 originalPosition;
    private void OnEnable()
    {
        RSE_CallFriendEvent.action += FriendEvent;
    }
    private void OnDisable()
    {
        RSE_CallFriendEvent.action -= FriendEvent;
    }
    private void Start()
    {
        originalPosition = gameObject.transform.localPosition;
    }
    private void FriendEvent()
    {
        RSE_PlaySoundFriend?.RaiseEvent();
        StartCoroutine(StartShake(shakeDuration, shakeMagnitude));
    }
    IEnumerator StartShake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude / 2;

            test.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        test.transform.localPosition = originalPosition;
    }
}