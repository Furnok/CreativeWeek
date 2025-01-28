using UnityEngine;

public class StaticScriptableObject<T> : ScriptableObject
{
    public T _value;

    public T Value
    {
        get => _value;
    }
}