using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SerializableDictionaryItem<TKey, TValue>
{
    public TKey key;
    public TValue value;
    public SerializableDictionaryItem(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<SerializableDictionaryItem<TKey, TValue>> serializableItems = new();

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        Clear();
        foreach (SerializableDictionaryItem<TKey, TValue> item in serializableItems)
        {
            if (item.key == null)
                continue;
            this[item.key] = item.value;
        }
    }
}
