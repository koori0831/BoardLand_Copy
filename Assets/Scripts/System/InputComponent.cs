using Core;
using System;

public record struct PlayerInputEvent(EPlayerActionType ActionType) : IEvent;
public class InputComponent
{
    private Action<EPlayerActionType> inputCallback;
    public bool IsEnabled { get; private set; }

    public void Enable(Action<EPlayerActionType> callback)
    {
        inputCallback = callback;
        if (IsEnabled)
            return;

        IsEnabled = true;
        EventBus<PlayerInputEvent>.Subscribe(OnInput);
    }

    public void Disable()
    {
        if (IsEnabled == false)
            return;

        IsEnabled = false;
        inputCallback = null;
        EventBus<PlayerInputEvent>.Unsubscribe(OnInput);
    }

    private void OnInput(PlayerInputEvent inputEvent)
    {
        Disable();
        inputCallback?.Invoke(inputEvent.ActionType);
    }
}
