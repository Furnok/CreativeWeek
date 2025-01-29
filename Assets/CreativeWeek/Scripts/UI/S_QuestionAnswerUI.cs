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


    [Header("SSO")]
    [SerializeField] SSO_TimeBetweenCharactereDisplay _ssoTimeBetweenCharactereDisplay;
    [SerializeField] SSO_TimerAnswer _ssoTimeToAnswer;

    List<S_Answer>  _answersList = new List<S_Answer>();
    Coroutine _timerCoroutine;
    private void Start()
    {
        _rseQuestionGenerate.action += DisplayQuestionAnswer;
        _rseOnDateAnswering.action += DisplayDateAnswer;
        _rseOnAnswerGive.action += StopTimerCoroutine;
    }

    private void OnDestroy()
    {
        _rseQuestionGenerate.action -= DisplayQuestionAnswer;
        _rseOnDateAnswering.action -= DisplayDateAnswer;
        _rseOnAnswerGive.action -= StopTimerCoroutine;


    }
    void DisplayQuestionAnswer(Question question)
    {
        StartCoroutine(QuestionDisplay(question));
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

        _timerCoroutine = StartCoroutine(SliderTimerToAnwer(question));


    }

    IEnumerator TextDisplay(string textToDIsplay)
    {
        _textDate.text = "";

        for (int i = 0; i < textToDIsplay.Length; i++)
        {
            _textDate.text += textToDIsplay[i];

            yield return new WaitForSeconds(_ssoTimeBetweenCharactereDisplay.Value);
        }
    }

    IEnumerator SliderTimerToAnwer(Question question)
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

    void DisplayDateAnswer(string textToDisplay)
    {

        StartCoroutine(TextDisplay(textToDisplay));

    }

    void ClearAnswer()
    {
        //_textQuestion.text = "";

        foreach (var answer in _answersList)
        {
            Destroy(answer.gameObject);
        }
        _answersList.Clear();
    }
}
