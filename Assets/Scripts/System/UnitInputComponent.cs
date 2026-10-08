using Core;
using System;

public record struct PlayerInputEvent(EPlayerInputType ActionType, int DiceIndex) : IEvent;
public class UnitInputComponent
{
    private Action<EPlayerInputType, int> inputCallback;
    public bool IsEnabled { get; private set; }

    public void Enable(Action<EPlayerInputType, int> callback)
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
        inputCallback?.Invoke(inputEvent.ActionType, inputEvent.DiceIndex);
        Disable();
    }
}
