using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharmManager : MonoBehaviour
{
    [Header("RSE")]
    [SerializeField] RSE_UpdateCharm RSE_UpdateCharm;
    [SerializeField] RSE_CheckWinDate RSE_CheckWinDate;
    [SerializeField] RSE_CallWinDate RSE_CallWinDate;
    [SerializeField] RSE_CallLoseDate RSE_CallLoseDate;
    [SerializeField] RSE_UpdateUICharm RSE_UpdateUICharm;
    [Header("SSO")]
    [SerializeField] SSO_Charm SSO_Charm;
    [SerializeField] SSO_WinCondition SSO_WinCondition;
    [Header("RSO")]
    [SerializeField] RSO_Charm RSO_Charm;

    private void OnEnable()
    {
        RSE_UpdateCharm.action += UpdateCharmValue;
        RSE_CheckWinDate.action += CheckWinCondition;
    }

    private void OnDisable()
    {
        RSE_UpdateCharm.action -= UpdateCharmValue;
        RSE_CheckWinDate.action -= CheckWinCondition;
    }
    private void Start()
    {
        RSO_Charm.Value = SSO_Charm.Value;
        RSE_UpdateUICharm.RaiseEvent();

    }

    private void UpdateCharmValue(int charmValue)
    {
        RSO_Charm.Value = Mathf.Clamp(RSO_Charm.Value + charmValue, 0, 100);
        RSE_UpdateUICharm?.RaiseEvent();
        if (RSO_Charm.Value <= 0)
        {
            RSE_CallLoseDate?.RaiseEvent();
        }
    }
    private void CheckWinCondition()
    {
        if(RSO_Charm.Value >= SSO_WinCondition.Value)
        {
            RSE_CallWinDate?.RaiseEvent();
        }
        else
        {
            RSE_CallLoseDate?.RaiseEvent();
        }
    }
}
