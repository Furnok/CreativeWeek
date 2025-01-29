using UnityEngine;

public class S_DisturbingEventManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_CallDoEvent RSE_CallDoEvent;
    [SerializeField] RSE_ChooseDoEvent RSE_ChooseDoEvent;

    [Header("RSO")]
    [SerializeField] RSO_DistrubingEventDone RSO_DistrubingEventDone;

    [Header("SSO")]
    [SerializeField] SSO_ListDisturbingEvent SSO_ListDisturbingEvent;

    private int doEvent;
    private int rndEvent;
    private DisturbingActifEvent disturbingActifEvent;
    private void OnEnable()
    {
        RSE_ChooseDoEvent.action += ChooseDoEvent;
    }
    private void OnDisable()
    {
        RSE_ChooseDoEvent.action -= ChooseDoEvent;
    }
    private void OnDestroy()
    {
        RSO_DistrubingEventDone.Value.Clear();
    }
    private void Start()
    {
        RSO_DistrubingEventDone.Value.Clear();
    }
    private void ChooseDoEvent()
    {
        doEvent = Random.Range(0, 2);
        if (doEvent == 1)
        {
            ChooseRandomDisturbingEvent();
        }
    }
    private void ChooseRandomDisturbingEvent()
    {
        rndEvent = Random.Range(0, SSO_ListDisturbingEvent.Value.Count);
        disturbingActifEvent = SSO_ListDisturbingEvent.Value[rndEvent];
        if (RSO_DistrubingEventDone.Value.Contains(disturbingActifEvent))
        {
            ChooseRandomDisturbingEvent();
        }
        else
        {
            RSO_DistrubingEventDone.Value.Add(disturbingActifEvent);
            RSE_CallDoEvent?.RaiseEvent(disturbingActifEvent);
        }
    }
}