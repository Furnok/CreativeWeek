using UnityEngine;

public class S_AudioManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] AudioSource SFXAudioSource;
    [SerializeField] AudioClip SFX_Ambulance;
    [SerializeField] AudioClip SFX_Friend;
    [SerializeField] AudioClip SFX_Mariachi;

    [Header("RSE")]
    [SerializeField] RSE_PlaySound RSE_PlaySoundAmbulance;
    [SerializeField] RSE_PlaySound RSE_PlaySoundFriend;
    [SerializeField] RSE_PlaySound RSE_PlaySoundMariachi;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_PlaySoundAmbulance.action += PlaySoundAmbulance;
        RSE_PlaySoundFriend.action += PlaySoundFriend;
        RSE_PlaySoundMariachi.action += PlaySoundMariachi;
    }
    private void OnDisable()
    {
        RSE_PlaySoundAmbulance.action -= PlaySoundAmbulance;
        RSE_PlaySoundFriend.action -= PlaySoundFriend;
        RSE_PlaySoundMariachi.action -= PlaySoundMariachi;
    }
    private void PlaySoundAmbulance()
    {
        SFXAudioSource.PlayOneShot(SFX_Ambulance);
    }
    private void PlaySoundFriend()
    {
        SFXAudioSource.PlayOneShot(SFX_Friend);
    }
    private void PlaySoundMariachi()
    {
        SFXAudioSource.PlayOneShot(SFX_Mariachi);
    }
}