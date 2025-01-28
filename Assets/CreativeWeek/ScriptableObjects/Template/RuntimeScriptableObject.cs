using System;
using System.Collections.Generic;
using UnityEngine;

namespace BT.ScriptablesObject
{
    public class RuntimeScriptableObject<T> : ScriptableObject
    {
        public Action<T> onValueChanged;

        [SerializeField] private T _value;

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
}
