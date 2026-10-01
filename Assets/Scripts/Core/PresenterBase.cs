// T: 외부에서 모델의 값을 변경하기 위한 IEvent.
// V: 이 MVP에서 사용하는 데이터 형식(ex. struct TitleUIData)
public abstract class PresenterBase<TEvent, TModelData> where TEvent : IUIEvent<TModelData>
{
    protected ViewBase view;
    protected ModelBase<TEvent, TModelData> model;

    public virtual void Initialize(ViewBase view, ModelBase<TEvent, TModelData> model)
    {
        Dispose();
        this.view = view;
        this.model = model;
        model.Property.Subscribe(OnModelValueChanged);
        OnModelValueChanged(model.Property.Value);
    }

    public virtual void OnModelValueChanged(TModelData value)
    {
    }

    public virtual void Dispose()
    {
        model?.Property.Unsubscribe(OnModelValueChanged);
        model = null;
        view = null;
    }
}
