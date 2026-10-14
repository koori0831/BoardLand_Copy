using Core;
using UnityEngine;

public class TitlePopupView : ViewBase
{
    [SerializeField] private PlayTypeSelectPopupUI playTypeSelectPopupUI;
    [SerializeField] private SettingPopupUI settingPopupUI;
    private TitlePopupPresenter titlePresenter;
    public override void Initialize(IPresenter presenter)
    {
        base.Initialize(presenter);
        titlePresenter = presenter as TitlePopupPresenter;
        if (titlePresenter == null)
        {
            LogManager.Log("Presenter is not of the expected type.", ELogType.UI, ELogCategory.TitlePopup, ELogLevel.Error);
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

    public void OpenPlayTypeSelectPopup()
    {
        playTypeSelectPopupUI.OpenPopup();
    }
    public void OpenSettingPopup()
    {
        settingPopupUI.OpenPopup();
    }
}
