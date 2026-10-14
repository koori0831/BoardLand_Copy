using Core;
using UnityEngine;

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
            return;
        }
    }

    public void OnClickStartButton()
    {
        titleView.OpenPlayTypeSelectPopup();
    }
    public void OnClickSettingsButton()
    {
        titleView.OpenSettingPopup();
    }
    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public override void Dispose()
    {
        titleView = null;
        titleModel = null;
        base.Dispose();
    }
}
