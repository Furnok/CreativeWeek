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
    [SerializeField] RSE_UpdateCharm _rseUpdateCharm;
    [SerializeField] RSE_OnDateAnswering _rseOnDateAnswering;
    [SerializeField] RSE_ProfilStateChange _rseProfilStateChange;

    [SerializeField] RSE_GenerateSpeech _rseGenerateSpeech;
    [SerializeField] RSE_GenerateQuestionSpeech _rseGenerateQuestionSpeech;

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
        _rsoSpeechSays.Value.Clear();
        _speechQuestionsAlreadyPosed.Clear();
        _rsoSpeachPitchQuestion.Value = new List<SpeechQuestion>(_ssoSpeachPitchQuestion.Value);

        _rseOnSpeechAnswerGive.action += TcheckAnswer;

        _rseGenerateSpeech.action += GenerateSpeech;
        _rseGenerateQuestionSpeech.action += GeneratQuestionAboutSpeech;
        //StartCoroutine(test());
    }

    private void OnDestroy()
    {
        _rsoSpeachPitchQuestion.Value.Clear();
        _rseOnSpeechAnswerGive.action -= TcheckAnswer;

        _rseGenerateSpeech.action -= GenerateSpeech;
        _rseGenerateQuestionSpeech.action -= GeneratQuestionAboutSpeech;

    }

    IEnumerator test()
    {
        _rsoCurrentProfile.Value.ProfilType = ProfilType.Street;

        yield return new WaitForEndOfFrame();

        GenerateSpeech();

        yield return new WaitForSeconds(5);

        GeneratQuestionAboutSpeech();
    }
    void GenerateSpeech()
    {
        
        if (/*_rsoSpeachPitchQuestion.Value != null ||*/ !_rsoSpeachPitchQuestion.Value.Any(x => x.ProfilType == _rsoCurrentProfile.Value.ProfilType))
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
        _rseOnQuestionSpeechGenerate.RaiseEvent(GetUniqueRandomElement());
        //GetUniqueRandomElement();



    }

    void TcheckAnswer(SpeechAnswer speechAnswer)
    {
        Debug.Log("a");
        if(speechAnswer.IsAnswerCorrect == true)
        {
            _rseUpdateCharm.RaiseEvent(speechAnswer.CharmeAnswerGive);

            //_rseOnDateAnswering.RaiseEvent(speechAnswer.ReplyContent);

            _rseProfilStateChange.RaiseEvent(ProfilState.Happy);
        }
        else
        {
            _rseUpdateCharm.RaiseEvent(speechAnswer.CharmeAnswerGive);

            //_rseOnDateAnswering.RaiseEvent(speechAnswer.ReplyContent);

            _rseProfilStateChange.RaiseEvent(ProfilState.Angry);
        }
    }

    public SpeechQuestion GetUniqueRandomElement()
    {
        if (_rsoSpeachPitchQuestion == null || _rsoSpeachPitchQuestion.Value == null)
        {
            Debug.LogError("_rsoSpeachPitchQuestion.Value est null ! Assurez-vous qu'il est bien assigné.");
            return default;
        }

        List<SpeechQuestion> availableElements = _rsoSpeechSays.Value
            .FindAll(item => !_speechQuestionsAlreadyPosed.Contains(item));

        if (availableElements.Count == 0)
        {
            Debug.LogWarning("Aucun élément unique disponible !");
            return default;
        }

        int randomIndex = Random.Range(0, availableElements.Count);
        SpeechQuestion selectedElement = availableElements[randomIndex];

        if (selectedElement.SpeechQuestionContent == null)
        {
            Debug.LogError("L'élément sélectionné a un PitchQuestionContent null ! Vérifiez vos données.");
            return default;
        }

        _speechQuestionsAlreadyPosed.Add(selectedElement);

        return selectedElement;
    }
}