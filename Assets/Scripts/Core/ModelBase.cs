// 외부 시스템에서 UI 상태 변경을 요청할 때 사용하는 이벤트.
public interface IUIEvent<T> : IEvent
{
    T GetChangedValue();
}

public abstract class ModelBase<TEvent, TModelData> where TEvent : IUIEvent<TModelData>
{
    public ObservableProperty<TModelData> Property { get; } = new();

    public virtual void Initialize()
    {
        EventBus<TEvent>.Subscribe(OnEventInvoke);
    }

    public virtual void OnEventInvoke(TEvent evt)
    {
        SetData(evt.GetChangedValue());
    }

    public virtual void SetData(TModelData value)
    {
        Property.Value = value;
    }

    public virtual void Dispose()
    {
        EventBus<TEvent>.Unsubscribe(OnEventInvoke);
    }
}
