using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DisturbingActifEvent
{
    public string NameEvent;
    public string EventContent;
    public float TimeToAnswer;
    public Sprite Sprite;
    public List<ActifEventAnswer> ActifEventAnswer;
}
