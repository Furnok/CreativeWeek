using UnityEngine;
using UnityEngine.Audio;

public class S_UIToilet : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] GameObject toiletPanel;
    [SerializeField] AudioMixer audioMixer;

    [Header("RSE")]
    [SerializeField] RSE_EnterToilet RSE_EnterToilet;
    [SerializeField] RSE_ExitToilet RSE_ExitToilet;

    //[Header("RSO")]

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_EnterToilet.action += EnterToilet;
        RSE_ExitToilet.action += ExitToilet;
    }
    private void OnDisable()
    {
        RSE_EnterToilet.action -= EnterToilet;
        RSE_ExitToilet.action -= ExitToilet;
    }
    private void EnterToilet()
    {
        toiletPanel.SetActive(true);
    }
    private void ExitToilet()
    {
        toiletPanel.SetActive(false);
    }
}