using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DateStep
{
    Presentation,
    Starter,
    Dish,
    Dessert,
    Bill
}


[System.Serializable]
public struct Question
{
    public string QuestionName;
    public string QuestionContent;
    public DateStep QuestionDateStep;
    public List<Answer> Answers;
    //public bool IsNeedConditionToValid;
    //public Condition ConditionToValid;
}
