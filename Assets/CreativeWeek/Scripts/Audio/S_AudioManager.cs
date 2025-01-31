using UnityEngine;

public class S_AudioManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] AudioSource MusicAudioSource;
    [SerializeField] AudioClip Music_Preparation;
    [SerializeField] AudioSource SFXAudioSource;
    [SerializeField] AudioClip SFX_Ambulance;
    [SerializeField] AudioClip SFX_Friend;
    [SerializeField] AudioClip SFX_Mariachi;
    [SerializeField] AudioClip SFX_Notification;
    [SerializeField] AudioClip SFX_MatchDate;

    [Header("RSE")]
    [SerializeField] RSE_PlaySound RSE_PlaySoundAmbulance;
    [SerializeField] RSE_PlaySound RSE_PlaySoundFriend;
    [SerializeField] RSE_PlaySound RSE_PlaySoundMariachi;
    [SerializeField] RSE_PlaySound RSE_PlaySoundNotification;
    [SerializeField] RSE_PlaySound RSE_PlaySoundMatchDate;
    [SerializeField] RSE_PlaySound RSE_PlayMusicPreparation;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_PlaySoundAmbulance.action += PlaySoundAmbulance;
        RSE_PlaySoundFriend.action += PlaySoundFriend;
        RSE_PlaySoundMariachi.action += PlaySoundMariachi;
        RSE_PlayMusicPreparation.action += PlayMusicPreparation;
        RSE_PlaySoundNotification.action += PlaySoundNotification;
        RSE_PlaySoundMatchDate.action += PlaySoundMatchDate;
    }
    private void OnDisable()
    {
        RSE_PlaySoundAmbulance.action -= PlaySoundAmbulance;
        RSE_PlaySoundFriend.action -= PlaySoundFriend;
        RSE_PlaySoundMariachi.action -= PlaySoundMariachi;
        RSE_PlayMusicPreparation.action -= PlayMusicPreparation;
        RSE_PlaySoundNotification.action -= PlaySoundNotification;
        RSE_PlaySoundMatchDate.action -= PlaySoundMatchDate;
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
    private void PlaySoundNotification()
    {
        SFXAudioSource.PlayOneShot(SFX_Notification);
    }
    private void PlaySoundMatchDate()
    {
        SFXAudioSource.PlayOneShot(SFX_MatchDate);
    }
    private void PlayMusicPreparation()
    {
        MusicAudioSource.PlayOneShot(Music_Preparation);
    }
}