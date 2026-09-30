public abstract class ModelBase<T, V> where T : IUIEvent<V>
{
    public ObservableProperty<V> Property = new();

    public virtual void Initialize()
    {
        EventBus<T>.Subscribe(OnEventInvoke);
    }
    public virtual void OnEventInvoke(T evt)
    {
        Property.Value = evt.GetChangedValue();
    }
    public virtual void Dispose()
    {
        EventBus<T>.Unsubscribe(OnEventInvoke);
    }
}