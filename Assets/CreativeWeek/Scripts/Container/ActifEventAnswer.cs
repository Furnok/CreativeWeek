using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct ActifEventAnswer
{
    public string AnswerName;
    public string AnswerButtonContent;
    public List<ReplyByDateType> ReplyByDateTypes;

    public bool CanUseItems;
    public int ItemIdLink;

}


[Serializable]
public struct ReplyByDateType
{
    public ProfilType ProfilType;
    public string DateReplyThought;
    public int CharmeAdd;

}
