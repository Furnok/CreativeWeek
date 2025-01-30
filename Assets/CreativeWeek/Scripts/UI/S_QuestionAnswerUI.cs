using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S_QuestionAnswerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _textDate; // text and question 
    [SerializeField] TextMeshProUGUI _textTimeLeft;


    [SerializeField] Slider _sliderTimeToAnwser;
    [SerializeField] GameObject _sliderTimeFill;
    [SerializeField] Transform _answerContentParents;
    [SerializeField] S_Answer _answer;

    [Header("RSE")]
    [SerializeField] RSE_OnQuestionGenerate _rseQuestionGenerate;
    [SerializeField] RSE_OnDateAnswering _rseOnDateAnswering;
    [SerializeField] RSE_OnAnswerGive _rseOnAnswerGive;
    [SerializeField] RSE_OnTimerQuestionEnd _rseTimerQuestionEnd;
    [SerializeField] RSE_OnSpeechQuestionCreate _rseOnSpeechQuestionCreate;
    [SerializeField] RSE_OnQuestionSpeechGenerate _rseOnQuestionSpeechGenerate;

    [SerializeField] RSE_DelayGenerateQuestion _rseDelayGenerateQuestion;
    [SerializeField] RSE_DelayGenerateSpeechQuestion _rseDelayGenerateSpeechQuestion;
    [SerializeField] RSE_DelayGenerateSpeech _rseDelayGenerateSpeech;

    [SerializeField] RSE_CheckWinDate _rseCheckWinDate;

    [SerializeField] RSE_UpdateCharm _rseUpdateCharm;

    [SerializeField] RSE_OnBadPresentation _OnBadPresentation;
    [SerializeField] RSE_OnGoodPresentation _OnGoodPresentation;

    [Header("RSO")] RSO_CurrentDateStep _rsoCurrentDateStep;


    [Header("SSO")]
    [SerializeField] SSO_TimeBetweenCharactereDisplay _ssoTimeBetweenCharactereDisplay;
    [SerializeField] SSO_TimerAnswer _ssoTimeToAnswer;

    List<S_Answer>  _answersList = new List<S_Answer>();
    Coroutine _timerCoroutine;
    private void Start()
    {
        _rseQuestionGenerate.action += DisplayQuestionAnswer;
        _rseOnSpeechQuestionCreate.action += DisplaySpeechContent;
        _rseOnDateAnswering.action += DisplayDateAnswer;
        _rseOnAnswerGive.action += StopTimerCoroutine;
        _rseOnQuestionSpeechGenerate.action += DisplaySpeechQuestionAnswer;

        _OnBadPresentation.action += StartDisplayingPresentationBad;
        _OnGoodPresentation.action += StartDisplayingPresentationGood;
    }

    private void OnDestroy()
    {
        _rseQuestionGenerate.action -= DisplayQuestionAnswer;
        _rseOnSpeechQuestionCreate.action -= DisplaySpeechContent;
        _rseOnDateAnswering.action -= DisplayDateAnswer;
        _rseOnAnswerGive.action -= StopTimerCoroutine;
        _rseOnQuestionSpeechGenerate.action -= DisplaySpeechQuestionAnswer;

        _OnBadPresentation.action -= StartDisplayingPresentationBad;
        _OnGoodPresentation.action -= StartDisplayingPresentationGood;
    }
    void DisplayQuestionAnswer(Question question)
    {
        StartCoroutine(QuestionDisplay(question));
    }
    void DisplaySpeechContent(SpeechQuestion speechQuestion)
    {

        StartCoroutine(TextDisplay(speechQuestion.SpeechContent));
    }
    IEnumerator DsiplayContent(string speechContent)
    {
        yield return StartCoroutine(TextDisplay(speechContent));

        _rseDelayGenerateQuestion.RaiseEvent();
    }

    void DisplaySpeechQuestionAnswer(SpeechQuestion speechQuestion)
    {

        StartCoroutine(DisplaySpeechQuestion(speechQuestion));


    }

    IEnumerator DisplaySpeechQuestion(SpeechQuestion speechQuestion)
    {
        yield return StartCoroutine(SpeechQuestionDisplay(speechQuestion));


        _rseDelayGenerateSpeech.RaiseEvent();
    }
    IEnumerator QuestionDisplay(Question question)
    {
        
        _textDate.text = "";

        for (int i = 0; i < question.QuestionContent.Length; i++)
        {
            _textDate.text += question.QuestionContent[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }
        DisplayResponseOption(question);

        _timerCoroutine = StartCoroutine(SliderTimerToAnwer());
    }

    IEnumerator SpeechQuestionDisplay(SpeechQuestion speechQuestion)
    {
        

        _textDate.text = "";

        for (int i = 0; i < speechQuestion.SpeechQuestionContent.Length; i++)
        {
            _textDate.text += speechQuestion.SpeechQuestionContent[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }
        DisplaySpeechResponseOption(speechQuestion);

        _timerCoroutine = StartCoroutine(SliderTimerToAnwer());
    }


    IEnumerator TextDisplay(string textToDIsplay)
    {
        _textDate.text = "";

        for (int i = 0; i < textToDIsplay.Length; i++)
        {
            _textDate.text += textToDIsplay[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }

        yield return new WaitForSeconds(2f);

        _textDate.text = "";

    }

    IEnumerator SliderTimerToAnwer()
    {
        float currentTimeToRespond = _ssoTimeToAnswer.Value;

        _sliderTimeToAnwser.maxValue = currentTimeToRespond;
        _sliderTimeToAnwser.value = currentTimeToRespond;

        float elapsedTime = 0f;

        while (elapsedTime < _ssoTimeToAnswer.Value)
        {
            elapsedTime += Time.deltaTime;

            _textTimeLeft.text = Mathf.Lerp(_ssoTimeToAnswer.Value, 0f, elapsedTime / _ssoTimeToAnswer.Value).ToString("F2") + "s";
            _sliderTimeToAnwser.value = Mathf.Lerp(_ssoTimeToAnswer.Value, 0f, elapsedTime / _ssoTimeToAnswer.Value);

            currentTimeToRespond = Mathf.Lerp(_ssoTimeToAnswer.Value, 0f, elapsedTime / _ssoTimeToAnswer.Value);

            //if (timeToRespond <= _timeTicking)
            //{
            //    _sliderTimeFill.transform.GetComponent<Image>().color = Color.red;
            //    //_audioSource.PlayOneShot(_audioTickingClicp);
            //}

            yield return null;
        }

        _rseTimerQuestionEnd.RaiseEvent();

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
        ClearAnswer();
    }

    void DisplayResponseOption(Question question)
    {
        foreach(Answer answer in question.Answers)
        {
            var answerScript = Instantiate(_answer, Vector3.zero,Quaternion.identity, _answerContentParents);

            _answersList.Add(answerScript);

            answerScript.InitializeAnswer(answer);

        }
    }

    void DisplaySpeechResponseOption(SpeechQuestion speechQuestion)
    {
        foreach (SpeechAnswer answer in speechQuestion.SpeechAnswers)
        {
            var answerScript = Instantiate(_answer, Vector3.zero, Quaternion.identity, _answerContentParents);

            _answersList.Add(answerScript);

            answerScript.InitializeAnswer(answer);

        }
    }

    void DisplayDateAnswer(string textToDisplay)
    {

        StartCoroutine(DisplayDate(textToDisplay));

    }

    //IEnumerator

    IEnumerator DisplayDate(string textToDisplay)
    {
        yield return StartCoroutine(TextDisplay(textToDisplay));

        if(_rsoCurrentDateStep.Value == DateStep.Bill)
        {
            //rseGameTcheckVlaueCharmIfWinOrNotEvent
            _rseCheckWinDate.RaiseEvent();
        }
        else
        {
            _rsoCurrentDateStep.Value = (DateStep)((int)_rsoCurrentDateStep.Value + 1);

        }

        _rseDelayGenerateSpeechQuestion.RaiseEvent();

    }

    void ClearAnswer()
    {
        _textDate.text = "";

        foreach (var answer in _answersList)
        {
            Destroy(answer.gameObject);
        }
        _answersList.Clear();
    }

    void StartDisplayingPresentationBad(string text)
    {
        StartCoroutine(TextPresentationDisplayIfBad(text));
    }

    void StartDisplayingPresentationGood(string text)
    {
        StartCoroutine(TextPresentationDisplayIfGood(text));
    }


    IEnumerator TextPresentationDisplayIfGood(string textToDIsplay)
    {
        _textDate.text = "";

        for (int i = 0; i < textToDIsplay.Length; i++)
        {
            _textDate.text += textToDIsplay[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }

        yield return new WaitForSeconds(1f);

        _textDate.text = "";

        _rseDelayGenerateSpeech.RaiseEvent();
    }

    IEnumerator TextPresentationDisplayIfBad(string textToDIsplay)
    {
        _textDate.text = "";

        for (int i = 0; i < textToDIsplay.Length; i++)
        {
            _textDate.text += textToDIsplay[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }

        yield return new WaitForSeconds(1f);

        _textDate.text = "";

        _rseUpdateCharm.RaiseEvent(-100);
    }
}
