using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_SpeechQuestionManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnSpeechQuestionCreate _rseOnSpeechQuestionCreate;
    [SerializeField] RSE_OnQuestionSpeechGenerate _rseOnQuestionSpeechGenerate;
    [SerializeField] RSE_OnSpeechAnswerGive _rseOnSpeechAnswerGive;

    [Header("RSO")]
    [SerializeField] RSO_SpeetchPitchQuestion _rsoSpeachPitchQuestion;
    [SerializeField] RSO_SpeechsSays _rsoSpeechSays;
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;

    [Header("SSO")]
    [SerializeField] SSO_SpeechPitchQuestion _ssoSpeachPitchQuestion;

    List<SpeechQuestion> _speechQuestionsAlreadyPosed = new List<SpeechQuestion>();

    private void Start()
    {
        _rsoSpeachPitchQuestion.Value.Clear();
        _rsoSpeachPitchQuestion.Value = _ssoSpeachPitchQuestion.Value;

        _rseOnSpeechAnswerGive.action += TcheckAnswer;
    }

    private void OnDestroy()
    {
        _rsoSpeachPitchQuestion.Value.Clear();
        _rseOnSpeechAnswerGive.action -= TcheckAnswer;


    }
    void GenerateSpeech()
    {

        if (_rsoSpeachPitchQuestion.Value != null || !_rsoSpeachPitchQuestion.Value.Any(x => x.ProfilType == _rsoCurrentProfile.Value.ProfilType))
        {
            Debug.LogError("Il n y a plus de peech disponible pour le profilType en question");
            return;
        }

        List<SpeechQuestion> speechListTarget = _rsoSpeachPitchQuestion.Value.Where(x => x.ProfilType == _rsoCurrentProfile.Value.ProfilType).ToList(); ;

        var speech = speechListTarget[Random.Range(0, speechListTarget.Count)];

        if (_rsoSpeechSays.Value.Contains(speech) == false)
        {
            _rsoSpeechSays.Value.Add(speech);
            _rsoSpeachPitchQuestion.Value.Remove(speech);
            _rseOnSpeechQuestionCreate.RaiseEvent(speech);
        }
        else
        {
            GenerateSpeech();
        }
    }

    void GeneratQuestionAboutSpeech()
    {
        var SpeechQuestion = _rsoSpeachPitchQuestion.Value.FirstOrDefault();
        //_rseOnQuestionSpeechGenerate.RaiseEvent();
    }

    void TcheckAnswer(SpeechAnswer speechAnswer)
    {

    }
}