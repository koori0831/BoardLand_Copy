using System;

public class TitlePopupPresenter : PresenterBase<TitlePopupEvent, TitlePopupData>
{
    private TitlePopupView titleView;
    private TitlePopupModel titleModel;

    public override void Initialize(ViewBase view, ModelBase<TitlePopupEvent, TitlePopupData> model)
    {
        if (!(view is TitlePopupView popupView))
            throw new ArgumentException("TitlePopupView is required.", nameof(view));
        if (!(model is TitlePopupModel popupModel))
            throw new ArgumentException("TitlePopupModel is required.", nameof(model));

        base.Initialize(view, model);
        titleView = popupView;
        titleModel = popupModel;
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
