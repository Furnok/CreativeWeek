using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_Answer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] TextMeshProUGUI _textReplyContent;

    [Header("RSE")]
    [SerializeField] RSE_OnAnswerGiveToQuestion _rseOnAnswerGiveToQuestion;
    [SerializeField] RSE_OnAnswerGive _rseOnAnswerGive;
    [SerializeField] RSE_OnSpeechAnswerGive _rseOnSpeechAnswerGive;


    Answer _answer;
    SpeechAnswer _speechAnswer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeAnswer(Answer answer)
    {
        _answer = answer;
        _speechAnswer = default;
        _textReplyContent.text = answer.ReplyContentText;
    }
    public void InitializeAnswer(SpeechAnswer speechAnswer)
    {
        _speechAnswer = speechAnswer;
        _answer = default;
        _textReplyContent.text = _speechAnswer.ReplyContent;
    }

    public void CallAnswerVerification()
    {
        if(_speechAnswer.Equals(default(SpeechAnswer)) == false)
        {
            Debug.Log("b");
            _rseOnAnswerGive.RaiseEvent();


            _rseOnSpeechAnswerGive.RaiseEvent(_speechAnswer);


        }
        if (_answer.Equals(default(Answer)) == false)
        {
            Debug.Log("c");
            _rseOnAnswerGive.RaiseEvent();

            _rseOnAnswerGiveToQuestion.RaiseEvent(_answer);


        }
    }
}
