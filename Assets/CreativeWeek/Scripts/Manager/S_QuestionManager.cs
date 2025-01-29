using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class S_QuestionManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    [Header("RSE")]
    [SerializeField] RSE_OnQuestionGenerate _rseQuestionGenerate;
    [SerializeField] RSE_OnAnswerGiveToQuestion _rseOnAnswerGiveToQuestion;
    [SerializeField] RSE_OnDateAnswering _rseOnDateAnswering;
    [SerializeField] RSE_OnTimerQuestionEnd _rseTimerQuestionEnd;
    [SerializeField] RSE_UpdateCharm _rseUpdateCharm;
    [SerializeField] RSE_ProfilStateChange _rseProfilStateChange;


    [Header("RSO")]
    [SerializeField] RSO_CurrentDateStep _rsoCurrentStep;
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;

    [Header("SSO")]
    [SerializeField] SSO_ListQuestionAnswer _ssoListQuestionAnswer;
    [SerializeField] SSO_ValuerRemoveIfNotAnswearingQuestion _ssoValuerRemoveIfNotAnswearingQuestion;
    [SerializeField] SSO_DateAnswerWhenTimerEnd _ssoDateAnswerWhenTimerEnd;


    private void Start()
    {
        _rseOnAnswerGiveToQuestion.action += TcheckAnswer;
        _rseTimerQuestionEnd.action += RemoveCharmeWhenNotAnswearing;

        CreateQuestion();
    }

    private void OnDestroy()
    {
        _rseOnAnswerGiveToQuestion.action -= TcheckAnswer;
        _rseTimerQuestionEnd.action -= RemoveCharmeWhenNotAnswearing;
    }
    void CreateQuestion()
    {
        var question = GetRandomQuestionWithEnum(_ssoListQuestionAnswer.Value, _rsoCurrentStep.Value);
        //Debug.Log("question");
        _rseQuestionGenerate.RaiseEvent(question);
    }

    void TcheckAnswer(Answer answer)
    {
        var currentProfil = _rsoCurrentProfile.Value;
        //var currentProfil = new Profil();
        //currentProfil.Intolerances = new List<IntoleranceType>();
        //currentProfil.Intolerances.Add(IntoleranceType.Gluten);
        //currentProfil.Intolerances.Add(IntoleranceType.Lactose);
        //currentProfil.Intolerances.Add(IntoleranceType.NutsFruits);

        var condition = answer.ConditionIn;

        if(condition.DietType != DietType.None && condition.IntoleranceType != null && condition.IntoleranceType.Count != 0)
        {
            if (condition.DietType == currentProfil.DietType && condition.IntoleranceType.Intersect(currentProfil.Intolerances).Any() == false)
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);

                _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);

                _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

                return;
            }
            else
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfNegatifReply);

                _rseUpdateCharm.RaiseEvent(answer.CharmeIfUnvalidAnswer);

                _rseProfilStateChange.RaiseEvent(ProfilState.Angry);

                return;
            }
        }
        else if (condition.DietType != DietType.None)
        {
            if (condition.DietType == currentProfil.DietType)
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

                return;
            }
            else
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfNegatifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfUnvalidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Angry);

                return;
            }
        }
        else if (condition.IntoleranceType != null && condition.IntoleranceType.Count != 0)
        {
            if (condition.IntoleranceType.Intersect(currentProfil.Intolerances).Any() == false)
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

                return;
            }
            else
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfNegatifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfUnvalidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Angry);

                return;
            }
        }

        if (condition.DrinkPreference != DrinkPreference.None)
        {
            if (condition.DrinkPreference == currentProfil.DrinkPreference)
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

                return;
            }
            else
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfNegatifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfUnvalidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Angry);

                return;
            }
        }
        if (condition.BillSeparation != BillSeparation.None)
        {
            if (condition.BillSeparation == currentProfil.BillSeparation)
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

                return;
            }
            else
            {
                _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfNegatifReply);
                _rseUpdateCharm.RaiseEvent(answer.CharmeIfUnvalidAnswer);
                _rseProfilStateChange.RaiseEvent(ProfilState.Angry);

                return;
            }
        }

        _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);
        _rseUpdateCharm.RaiseEvent(answer.CharmeIfValidAnswer);
        _rseProfilStateChange.RaiseEvent(ProfilState.Happy);

    }
    Question GetRandomQuestionWithEnum(List<Question> listQuestion, DateStep targetEnum)
    {
        var filteredList = listQuestion.FindAll(item => item.QuestionDateStep.Equals(targetEnum));

        if (filteredList.Count == 0)
        {
            Debug.LogWarning("Aucun élément ne correspond à l'enum spécifié !");
            return default;
        }

        int randomIndex = Random.Range(0, filteredList.Count);
        return filteredList[randomIndex];
    }
    
    void RemoveCharmeWhenNotAnswearing()
    {
        _rseUpdateCharm.RaiseEvent(_ssoValuerRemoveIfNotAnswearingQuestion.Value);
        _rseOnDateAnswering.RaiseEvent(_ssoDateAnswerWhenTimerEnd.Value[Random.Range(0, _ssoDateAnswerWhenTimerEnd.Value.Count)]);
    }
}