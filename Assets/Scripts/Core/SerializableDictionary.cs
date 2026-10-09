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

// 인스펙터에서는 serializableItems 리스트로 편집하고,
// 역직렬화 시점에 리스트 내용을 Dictionary로 옮겨 담는다.
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<SerializableDictionaryItem<TKey, TValue>> serializableItems = new();

    public void OnBeforeSerialize()
    {
        // 리스트가 원본 데이터. 인스펙터 편집 중 중복 키가 사라지지 않도록 여기서는 덮어쓰지 않는다.
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
