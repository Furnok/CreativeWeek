using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct PitchQuestion
{
    public string PitchQuestionName;
    public string PitchContent;
    public ProfilType ProfilType;
    public List<PitchAnswer> PitchAnswers;
}
