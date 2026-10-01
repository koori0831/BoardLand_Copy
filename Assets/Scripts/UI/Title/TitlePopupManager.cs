public class TitlePopupManager : MVPUIManagerBase<TitlePopupEvent, TitlePopupData>
{
    protected override void OnEnable()
    {
        model = new TitlePopupModel();
        presenter = new TitlePopupPresenter();
        base.OnEnable();
    }
}
