using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            LogManager.Log("View or Model is not of the expected type.", ELogType.UI, ELogCategory.TitlePopup, ELogLevel.Error);
            return;
        }
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnClickSettingsButton()
    {
    }
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public override void Dispose()
    {
        titleView = null;
        titleModel = null;
        base.Dispose();
    }
}
