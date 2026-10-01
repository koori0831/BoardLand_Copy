public class TitlePopupModel : ModelBase<TitlePopupEvent, TitlePopupData>
{
    public TitlePopupModel()
    {
        SetData(new TitlePopupData(IsOpen: true));
    }

    public void Open()
    {
        SetData(new TitlePopupData(IsOpen: true));
    }

    public void Close()
    {
        SetData(new TitlePopupData(IsOpen: false));
    }
}

public record struct TitlePopupEvent(TitlePopupData PopupData) : IUIEvent<TitlePopupData>
{
    public TitlePopupData GetChangedValue() => PopupData;
}

public record struct TitlePopupData(bool IsOpen);
