using UnityEngine;

public class StaticScriptableObject<T> : ScriptableObject
{
    private T _value;

    public T Value
    {
        get => _value;
    }
}