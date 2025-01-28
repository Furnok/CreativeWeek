using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Answer
{
    public string AnswerName;
    public string AnswerContent;
    public int CharmeValueGive;
    public bool CanUseItems;
    public List<ItemUseEffect> ListItems;
}


