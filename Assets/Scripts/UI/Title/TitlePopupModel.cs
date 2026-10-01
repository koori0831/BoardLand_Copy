using Core;

public class TitlePopupModel : ModelBase<TitlePopupEvent, TitlePopupData>
{
    public TitlePopupModel()
    {
    }
}

public record struct TitlePopupEvent(TitlePopupData PopupData) : IUIEvent<TitlePopupData>
{
    public TitlePopupData GetChangedValue() => PopupData;
}

public record struct TitlePopupData();
