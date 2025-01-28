using System;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeScriptableObject<T> : ScriptableObject
{
    public Action<T> onValueChanged;

    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_value, value)) return;

            _value = value;

            onValueChanged?.Invoke(_value);
        }
    }
}
