using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct ActifEventAnswer
{
    public string AnswerName;
    public bool CanUseItems;
    public List<ItemUseEffect> ListItems;

}
