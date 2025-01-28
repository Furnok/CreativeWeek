using System.Collections.Generic;
using UnityEngine;

public class S_QuestionManager : MonoBehaviour
{
    //[Header("Parameters")]

    //[Header("References")]

    //[Header("RSE")]

    [Header("RSO")]
    [SerializeField] RSO_CurrentDateStep _rsoCurrentStep;
    [SerializeField] RSO_CurrentProfile _rsoCurrentProfile;

    [Header("SSO")]
    [SerializeField] SSO_ListQuestionAnswer _ssoListQuestionAnswer;

    void CreateQuestion()
    {
        var question = GetRandomQuestionWithEnum(_ssoListQuestionAnswer.Value, _rsoCurrentStep.Value);
    }

    void TcheckAnswer(Answer answer)
    {

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
}