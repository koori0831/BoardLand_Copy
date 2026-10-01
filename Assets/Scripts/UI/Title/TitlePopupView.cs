using Core;

public class TitlePopupView : ViewBase
{
    private TitlePopupPresenter titlePresenter;
    public override void Initialize(IPresenter presenter)
    {
        base.Initialize(presenter);
        titlePresenter = presenter as TitlePopupPresenter;
        if (titlePresenter == null)
        {
            LogManager.Log("Presenter is not of the expected type.", ELogType.UI, ELogCategory.TitlePopup, ELogMessageType.Error);
            return;
        }
    }

    public void OnClickStartButton()
    {
        titlePresenter.OnClickStartButton();
    }
    public void OnClickSettingsButton()
    {
        titlePresenter.OnClickSettingsButton();
    }
    public void OnClickExitButton()
    {
        titlePresenter.OnClickExitButton();
    }
}
