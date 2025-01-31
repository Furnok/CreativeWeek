using TMPro;
using UnityEngine;

public class S_AnswerActifEvent : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] TextMeshProUGUI _textReplyContent;

    [Header("RSE")]
    [SerializeField] RSE_OnActifEventAnswerGive _rseOnActifEventAnswerGive;
    [SerializeField] RSE_OnActifEventAnswerGiveToEvent _rseOnActiveEventAnswerGiveToQuestion;


    ActifEventAnswer _actifEventAnswer;
   

   
    public void InitializeAnswer(ActifEventAnswer actifEventAnswer)
    {
        _actifEventAnswer = actifEventAnswer;

        _textReplyContent.text = _actifEventAnswer.AnswerButtonContent;
    }

    public void CallAnswerVerification()
    {
        if (_actifEventAnswer.Equals(default(ActifEventAnswer)) == false)
        {
            Debug.Log("evetnyes");
            _rseOnActifEventAnswerGive.RaiseEvent();

            _rseOnActiveEventAnswerGiveToQuestion.RaiseEvent(_actifEventAnswer);


        }
    }
}