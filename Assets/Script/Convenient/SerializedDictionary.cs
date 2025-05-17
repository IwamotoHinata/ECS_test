using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class SerializedDictionary<TKey, TValue>
{
    [SerializeField] private List<DicElement> _dicElements = new List<DicElement>();
    private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();


    [Serializable]
    public class DicElement
    {
        public TKey Key;
        public TValue Value;

        public DicElement() { } //MissingMethodException‰ñ”ð—p

        public DicElement(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public Dictionary<TKey, TValue> GetDictionary
    { 
        get
        {
            if (_dicElements != null)
            {
                _dictionary.Clear();
                foreach (DicElement element in _dicElements)
                {
                    _dictionary.Add(element.Key, element.Value);
                }
                return _dictionary;
            }
            else
                return null;
        }
    }
}