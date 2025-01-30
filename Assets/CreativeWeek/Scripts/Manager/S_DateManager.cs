using System.Collections;
using UnityEngine;

public class S_DateManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int _charmAddGoodPresentation;
    [SerializeField] int _charmRemoveBadPresentation;

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_GenerateQuestion _rseGenerateQuestion;
    [SerializeField] RSE_GenerateSpeech _rseGenerateSpeech;
    [SerializeField] RSE_GenerateQuestionSpeech _rseGenerateQuestionSpeech;

    [SerializeField] RSE_DelayGenerateQuestion _rseDelayGenerateQuestion;
    [SerializeField] RSE_DelayGenerateSpeech _rseDelayGenerateSpeech;
    [SerializeField] RSE_DelayGenerateSpeechQuestion _rseDelayGenerateSpeechQuestion;

    [SerializeField] RSE_UpdateCharm _rseUpdateCharm;

    [Header("RSO")]
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;
    [SerializeField] RSO_CurrentDateStep _rsoCurrentDateStep;

    [Header("SSO")]
    [SerializeField] RSO_CurrentListObject _ssoCurrentListObject;

    private void Start()
    {
        _rsoCurrentDateStep.Value = DateStep.Presentation;

        _rseDelayGenerateQuestion.action += GenerateQuestion;
        _rseDelayGenerateSpeech.action += GenerateSpeech;
        _rseDelayGenerateSpeechQuestion.action += GenerateSpeechQuestion;


        StartCoroutine(StartPresentation());
    }

    private void OnDestroy()
    {
        _rsoCurrentDateStep.Value = DateStep.Presentation;

        _rseDelayGenerateQuestion.action -= GenerateQuestion;
        _rseDelayGenerateSpeech.action -= GenerateSpeech;
        _rseDelayGenerateSpeechQuestion.action -= GenerateSpeechQuestion;
    }

    //Need id items to do 
    IEnumerator StartPresentation()
    {
        yield return new WaitForSeconds(2f);

        PresentationTcheck();

        yield return null;

        _rsoCurrentDateStep.Value = DateStep.Starter;

        GenerateSpeech();
    }

    void GenerateSpeech()
    {
        StartCoroutine(DelayGenerateSpeech());
    }
    void GenerateQuestion()
    {
        StartCoroutine(DelayGenerateQuestion());
    }

    void GenerateSpeechQuestion()
    {
        StartCoroutine(DelayGenerateSpeechQuestion());
    }

    IEnumerator DelayGenerateSpeech()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateSpeech.RaiseEvent();

        yield return null;
    }

    IEnumerator DelayGenerateQuestion()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateQuestion.RaiseEvent();

        yield return null;
    }

    IEnumerator DelayGenerateSpeechQuestion()
    {
        yield return new WaitForSeconds(2f);

        _rseGenerateQuestionSpeech.RaiseEvent();

        yield return null;
    }

    void PresentationTcheck()
    {

    }
}