using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct SpeechQuestion
{
    public string PitchQuestionName;
    public ProfilType ProfilType;
    public string PitchContent;
    public string PitchQuestionContent;
    public List<SpeechAnswer> PitchAnswers;
    //[HideInInspector] public bool IsQuestionAlreadySay = false;
}
