using UnityEngine;
public interface IUIEvent<T> : IEvent
{
    public T GetChangedValue();
}

// T: 외부에서 모델의 값을 변경 시키기 위한 IEvent.
// V: 이 MVP에서 사용하는 데이터 형식(ex. struct TitleUIData)
public abstract class PresenterBase<T, V> where T : IUIEvent<V>
{
    protected ViewBase<T, V> view;
    protected ModelBase<T, V> model;
    public virtual void Initialize(ViewBase<T, V> view, ModelBase<T, V> model)
    {
        this.view = view;
        this.model = model;
        model.Property.Subscribe(OnModelValueChanged);
    }
    public virtual void OnModelValueChanged(V value)
    { 
    }
    public virtual void Dispose()
    {
        model.Property.Unsubscribe(OnModelValueChanged);
    }
}
