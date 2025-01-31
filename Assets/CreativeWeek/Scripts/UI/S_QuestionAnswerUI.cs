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

    [SerializeField] RSE_IsTextDisturbed _rseIsTextDisturbEvent;

    [SerializeField] RSE_OnDateStepChange _onDateStepChange;

    [SerializeField] RSE_ChooseDoEvent _rseChooseDoEvent;

    [SerializeField] RSE_CallLoseDate _rseCallLoseDate;

    [Header("RSO")]
    [SerializeField] RSO_CurrentDateStep _rsoCurrentDateStep;


    [Header("SSO")]
    [SerializeField] SSO_TimeBetweenCharactereDisplay _ssoTimeBetweenCharactereDisplay;
    [SerializeField] SSO_TimerAnswer _ssoTimeToAnswer;

    List<S_Answer>  _answersList = new List<S_Answer>();
    Coroutine _timerCoroutine;
    bool _isTextDisturbingEventActive = false;

    private void Start()
    {
        _rseQuestionGenerate.action += DisplayQuestionAnswer;
        _rseOnSpeechQuestionCreate.action += DisplaySpeechContent;
        _rseOnDateAnswering.action += DisplayDateAnswer;
        _rseOnAnswerGive.action += StopTimerCoroutine;
        _rseOnQuestionSpeechGenerate.action += DisplaySpeechQuestionAnswer;

        _OnBadPresentation.action += StartDisplayingPresentationBad;
        _OnGoodPresentation.action += StartDisplayingPresentationGood;

        _rseIsTextDisturbEvent.action += SetIsTextDisturb;

        _rseCallLoseDate.action += GameStop;
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

        _rseIsTextDisturbEvent.action -= SetIsTextDisturb;

        _rseCallLoseDate.action -= GameStop;


    }
    void DisplayQuestionAnswer(Question question)
    {
        StartCoroutine(QuestionDisplay(question));
    }
    void DisplaySpeechContent(SpeechQuestion speechQuestion)
    {

        StartCoroutine(DsiplayContent(speechQuestion.SpeechContent));
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


        //_rseDelayGenerateSpeech.RaiseEvent(); //the buggg!!!
    } 
    IEnumerator QuestionDisplay(Question question)
    {
        
        _textDate.text = "";

        for (int i = 0; i < question.QuestionContent.Length; i++)
        {
            if(_isTextDisturbingEventActive == false)
            {
                _textDate.text += question.QuestionContent[i];

            }
            else
            {
                int randomCharacter = Random.Range(0, 4);
                if (randomCharacter == 0 && question.QuestionContent[i] != ' ')
                {
                    if (i > 0 && _textDate.text[i - 1] != '#')
                    {
                        _textDate.text += "#";
                    }
                    else
                    {
                        _textDate.text += question.QuestionContent[i];
                    }
                }
                else
                {
                    _textDate.text += question.QuestionContent[i];
                }
            }

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
            if (_isTextDisturbingEventActive == false)
            {
                _textDate.text += speechQuestion.SpeechQuestionContent[i];

            }
            else
            {
                int randomCharacter = Random.Range(0, 4);
                if (randomCharacter == 0 && speechQuestion.SpeechQuestionContent[i] != ' ')
                {
                    if (i > 0 && _textDate.text[i - 1] != '#')
                    {
                        _textDate.text += "#";
                    }
                    else
                    {
                        _textDate.text += speechQuestion.SpeechQuestionContent[i];
                    }
                }
                else
                {
                    _textDate.text += speechQuestion.SpeechQuestionContent[i];
                }
            }

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
            if (_isTextDisturbingEventActive == false)
            {
                _textDate.text += textToDIsplay[i];

            }
            else
            {
                int randomCharacter = Random.Range(0, 4);
                if (randomCharacter == 0 && textToDIsplay[i] != ' ')
                {
                    if (i > 0 && _textDate.text[i - 1] != '#')
                    {
                        _textDate.text += "#";
                    }
                    else
                    {
                        _textDate.text += textToDIsplay[i];
                    }
                }
                else
                {
                    _textDate.text += textToDIsplay[i];
                }
            }

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

    void SetIsTextDisturb(bool state)
    {
        _isTextDisturbingEventActive = state;
    }
    void StopTimerCoroutine()
    {
        StopCoroutine(_timerCoroutine);
        StopAllCoroutines();
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
        Debug.Log($"{_rsoCurrentDateStep.Value}");


        yield return StartCoroutine(TextDisplay(textToDisplay));
        Debug.Log(_rsoCurrentDateStep.Value.ToString());
        if (_rsoCurrentDateStep.Value == DateStep.Bill)
        {
            //rseGameTcheckVlaueCharmIfWinOrNotEvent
            _rseCheckWinDate.RaiseEvent();

            yield break;

        }
        else
        {
            _rsoCurrentDateStep.Value = (DateStep)((int)_rsoCurrentDateStep.Value + 1);

            _rseChooseDoEvent.RaiseEvent();

            _onDateStepChange.RaiseEvent();

        }

        if (_rsoCurrentDateStep.Value != DateStep.Bill)
        {
            
        }
        _rseDelayGenerateSpeechQuestion.RaiseEvent();
    }

    void ClearAnswer()
    {
        _textDate.text = "";
        _textTimeLeft.text = "";
        _sliderTimeToAnwser.value = 0;

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

    void GameStop()
    {
        ClearAnswer();
        StopAllCoroutines();
    }
}
