public record struct TitlePopupEvent(TitlePopupData PopupData) : IUIEvent<TitlePopupData>
{
    public TitlePopupData GetChangedValue()
    {
        return PopupData;
    }
}
public record struct TitlePopupData(bool test)
{
}

public class TitlePopupPresenter : PresenterBase<TitlePopupEvent, TitlePopupData>
{
    public override void OnModelValueChanged(TitlePopupData value)
    {
        base.OnModelValueChanged(value);
        if (value.test)
            view.ShowUIRoot();
        else
            view.HideUIRoot();
    }
}
