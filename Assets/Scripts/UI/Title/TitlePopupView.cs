using Core;
using System;
using UnityEngine.InputSystem;

public class TitlePopupView : ViewBase
{
    public event Action OpenRequested;
    public event Action CloseRequested;

    public void RequestOpen() => OpenRequested?.Invoke();

    public void RequestClose() => CloseRequested?.Invoke();

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            LogManager.Log("Space key pressed. Requesting open.", ELogType.UI, ELogCategory.TitlePopup);
            RequestOpen();
        }
        if (keyboard.enterKey.wasPressedThisFrame)
        {
            LogManager.Log("Enter key pressed. Requesting close.", ELogType.UI, ELogCategory.TitlePopup);
            RequestClose();
        }
    }
}
