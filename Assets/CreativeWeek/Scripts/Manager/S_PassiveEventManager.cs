using System;
using UnityEngine;
public enum DisturbingPassiveEvent
{
    Ambulance,
    Friend,
    Mariachi
}
public class S_PassiveEventManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_ChoosePassiveEvent RSE_ChoosePassiveEvent;
    [SerializeField] RSE_CallAmbulanceEvent RSE_CallAmbulanceEvent;
    [SerializeField] RSE_CallFriendEvent RSE_CallFriendEvent;
    [SerializeField] RSE_CallMariachiEvent RSE_CallMariachiEvent;
    //[Header("RSO")]

    //[Header("SSO")]
    private void OnEnable()
    {
        RSE_ChoosePassiveEvent.action += ChooseRandomPassiveEvent;
    }
    private void OnDisable()
    {
        RSE_ChoosePassiveEvent.action -= ChooseRandomPassiveEvent;
    }
    private void ChooseRandomPassiveEvent()
    {
        var randomEvent = GetRandomEnumValue<DisturbingPassiveEvent>();
        if(randomEvent == DisturbingPassiveEvent.Ambulance)
        {
            RSE_CallAmbulanceEvent?.RaiseEvent();
        }
        else if (randomEvent == DisturbingPassiveEvent.Friend)
        {
            RSE_CallFriendEvent?.RaiseEvent();
        }
        else if(randomEvent == DisturbingPassiveEvent.Mariachi)
        {
            RSE_CallMariachiEvent?.RaiseEvent();
        }
    }
    T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));

        int randomIndex = UnityEngine.Random.Range(0, values.Length);

        return (T)values.GetValue(randomIndex);
    }
}