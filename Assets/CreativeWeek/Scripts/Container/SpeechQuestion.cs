using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct SpeechQuestion
{
    public string SpeechQuestionName;
    public ProfilType ProfilType;
    public string SpeechContent;
    public string SpeechQuestionContent;
    public List<SpeechAnswer> SpeechAnswers;
    //[HideInInspector] public bool IsQuestionAlreadySay = false;
}
