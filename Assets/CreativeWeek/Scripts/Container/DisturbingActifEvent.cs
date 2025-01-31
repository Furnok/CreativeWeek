using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DisturbingActifEvent
{
    public string NameEvent;
    public string EventContent;
    public float TimeToAnswer;
    public Sprite Sprite;
    public AudioClip AudioClip;
    public Vector2 PositionSprite = new Vector2(-100, 0);
    public List<ActifEventAnswer> ActifEventAnswer;
}
