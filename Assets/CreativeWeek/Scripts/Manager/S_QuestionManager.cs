using System.Collections.Generic;
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
        Debug.Log("question");
        _rseQuestionGenerate.RaiseEvent(question);
    }

    void TcheckAnswer(Answer answer)
    {
        _rseOnDateAnswering.RaiseEvent(answer.DateAnswerIfPositifReply);//gonna chnage
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