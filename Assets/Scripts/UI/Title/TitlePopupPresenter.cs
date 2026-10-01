public class TitlePopupPresenter : PresenterBase<TitlePopupEvent, TitlePopupData>
{
    private TitlePopupView titleView;
    private TitlePopupModel titleModel;

    public override void Initialize(ViewBase view, ModelBase<TitlePopupEvent, TitlePopupData> model)
    {
        base.Initialize(view, model);
        titleView = this.view as TitlePopupView;
        titleModel = this.model as TitlePopupModel;

        if (titleView == null || titleModel == null)
        {
            LogManager.Log("View or Model is not of the expected type.", ELogType.UI, ELogCategory.TitlePopup, ELogMessageType.Error);
            return;
        }

        titleView.OpenRequested += OnOpenRequested;
        titleView.CloseRequested += OnCloseRequested;
    }

    public override void OnModelValueChanged(TitlePopupData value)
    {
        if (value.IsOpen)
            view.ShowUIRoot();
        else
            view.HideUIRoot();
    }

    private void OnOpenRequested() => titleModel.Open();

    private void OnCloseRequested() => titleModel.Close();

    public override void Dispose()
    {
        if (titleView != null)
        {
            titleView.OpenRequested -= OnOpenRequested;
            titleView.CloseRequested -= OnCloseRequested;
        }

        titleView = null;
        titleModel = null;
        base.Dispose();
    }
}
