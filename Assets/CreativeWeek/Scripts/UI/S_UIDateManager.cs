using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class S_UIDateManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [Header("RSE")]
    [SerializeField] RSE_CallWinDate RSE_CallWinDate;
    [SerializeField] RSE_CallLoseDate RSE_CallLoseDate;
    
    //[Header("RSO")]
    

    //[Header("SSO")]

    private void OnEnable()
    {
        RSE_CallWinDate.action += Win;
        RSE_CallLoseDate.action += Lose;
        
    }
    private void OnDisable()
    {
        RSE_CallWinDate.action -= Win;
        RSE_CallLoseDate.action -= Lose;
        
    }
    private void Win()
    {
        panelWin.SetActive(true);
    }
    private void Lose()
    {
        panelLose.SetActive(true);
    }

    
}