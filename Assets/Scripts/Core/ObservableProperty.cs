using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObservableProperty<T>
{
    [SerializeField]
    private T value;

    [field: NonSerialized]
    public event Action<T> Changed;

    public T Value
    {
        get => value;
        set => SetValue(value);
    }

    public ObservableProperty(T value = default)
    {
        this.value = value;
    }

    public void SetValue(T newValue)
    {
        if (EqualityComparer<T>.Default.Equals(value, newValue))
            return;

        value = newValue;
        Changed?.Invoke(value);
    }

    public void SetValueWithoutNotify(T newValue)
    {
        value = newValue;
    }

    public void Subscribe(Action<T> callback, bool notifyImmediately = false)
    {
        if (callback == null)
            return;

        Changed += callback;

        if (notifyImmediately)
            callback(value);
    }

    public void Unsubscribe(Action<T> callback)
    {
        if (callback == null)
            return;

        Changed -= callback;
    }
}