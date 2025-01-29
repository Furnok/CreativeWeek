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

    Answer _answer;
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
        _textReplyContent.text = answer.ReplyContentText;
    }

    public void CallAnswerVerification()
    {
        _rseOnAnswerGiveToQuestion.RaiseEvent(_answer);
        _rseOnAnswerGive.RaiseEvent();
    }
}
