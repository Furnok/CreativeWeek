using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct ActifEventAnswer
{
    public string AnswerName;
    public List<ReplyByDateType> ReplyByDateTypes;

    public bool CanUseItems;
    public int ItemIdLink;

}


[Serializable]
public struct ReplyByDateType
{
    public string DateReply;
    public ProfilType ProfilType;
}
