using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_DisturbingEventManager : MonoBehaviour
{
    //[Header("Parameters")]

    [Header("References")]
    [SerializeField] TextMeshProUGUI _textActifEvent; // text and question 
    [SerializeField] TextMeshProUGUI _textTimeLeft;
    [SerializeField] TextMeshProUGUI _textThought;


    [SerializeField] Slider _sliderTimeToAnwser;
    [SerializeField] GameObject _sliderTimeFill;
    [SerializeField] Transform _answerContentParents;

    [SerializeField] S_AnswerActifEvent _answerActifEvent;

    [Header("RSE")]
    [SerializeField] RSE_CallDoEvent RSE_CallDoEvent;
    [SerializeField] RSE_ChooseDoEvent RSE_ChooseDoEvent;

    [Header("RSO")]
    [SerializeField] RSO_DistrubingEventDone RSO_DistrubingEventDone;
    [SerializeField] RSO_CurrentProfile _rsoCureentProfil;
    [SerializeField] RSO_CurrentListObject _rsoCureentListObject;

    [Header("SSO")]
    [SerializeField] SSO_ListDisturbingEvent SSO_ListDisturbingEvent;
    [SerializeField] SSO_TimeBetweenCharactereDisplay _ssoTimeBetweenCharactereDisplay;

    private int doEvent;
    private int rndEvent;
    private DisturbingActifEvent disturbingActifEvent;

    Coroutine _timerCoroutine;
    List<S_AnswerActifEvent> _answersActifEventList = new List<S_AnswerActifEvent>();


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

    IEnumerator EventTextDisplaying(DisturbingActifEvent disturbingActifEvent)
    {


        _textActifEvent.text = "";

        for (int i = 0; i < disturbingActifEvent.EventContent.Length; i++)
        {
            _textActifEvent.text += disturbingActifEvent.EventContent[i];


            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);

        }

        _timerCoroutine = StartCoroutine(SliderTimerToAnwer(disturbingActifEvent));
    }


    void DisplayResponseOption(DisturbingActifEvent disturbingActifEvent)
    {
        foreach (ActifEventAnswer answer in disturbingActifEvent.ActifEventAnswer)
        {
            var answerScript = Instantiate(_answerActifEvent, Vector3.zero, Quaternion.identity, _answerContentParents);

            _answersActifEventList.Add(answerScript);

            answerScript.InitializeAnswer(answer);

        }
    }

    IEnumerator SliderTimerToAnwer(DisturbingActifEvent disturbingActifEvent)
    {
        float currentTimeToRespond = disturbingActifEvent.TimeToAnswer;//need timer of actf wuestion event

        _sliderTimeToAnwser.maxValue = currentTimeToRespond;
        _sliderTimeToAnwser.value = currentTimeToRespond;

        float elapsedTime = 0f;

        while (elapsedTime < disturbingActifEvent.TimeToAnswer)//need timer of actf wuestion event
        {
            elapsedTime += Time.deltaTime;

            _textTimeLeft.text = Mathf.Lerp(disturbingActifEvent.TimeToAnswer, 0f, elapsedTime / disturbingActifEvent.TimeToAnswer).ToString("F2") + "s";//need timer of actf wuestion event
            _sliderTimeToAnwser.value = Mathf.Lerp(disturbingActifEvent.TimeToAnswer, 0f, elapsedTime / disturbingActifEvent.TimeToAnswer);//need timer of actf wuestion event

            currentTimeToRespond = Mathf.Lerp(disturbingActifEvent.TimeToAnswer, 0f, elapsedTime / disturbingActifEvent.TimeToAnswer);//need timer of actf wuestion event

            //if (timeToRespond <= _timeTicking)
            //{
            //    _sliderTimeFill.transform.GetComponent<Image>().color = Color.red;
            //    //_audioSource.PlayOneShot(_audioTickingClicp);
            //}

            yield return null;
        }


        ClearAnswer();
        //_audioSource.Stop();

        _sliderTimeToAnwser.value = 0;
        //currentImpatientTime.CurrentImpatientTime = 0;

        _textTimeLeft.text = "";

        yield return null;
    }

    void StopTimerCoroutine()
    {
        StopCoroutine(_timerCoroutine);
        StopAllCoroutines();
        ClearAnswer();
    }

    void ClearAnswer()
    {
        _textActifEvent.text = "";
        _textTimeLeft.text = "";
        _sliderTimeToAnwser.value = 0;

        foreach (var answer in _answersActifEventList)
        {
            Destroy(answer.gameObject);
        }
        _answersActifEventList.Clear();
    }
}