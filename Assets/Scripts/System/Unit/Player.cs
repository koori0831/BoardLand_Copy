using Core;
using System;
using System.Collections.Generic;

public class Player : Unit
{
    // 가지고 있는 주사위 목록
    private List<Dice> diceInventory = null;
    // 가지고 있는 아이템(무기) 목록
    private List<Weapon> weaponInventory = null;

    private UnitInputComponent inputComponenet;
    private Action<PlayerInputResult> inputCallback;

    public Player(UnitStatus status) : base(status)
    {
        inputComponenet = new UnitInputComponent();
        diceInventory = new List<Dice>();
        weaponInventory = new List<Weapon>();
    }

    public void InputReady(Action<PlayerInputResult> inputCallback)
    {
        this.inputCallback = inputCallback;
        inputComponenet.Enable(OnInput);
    }

    public void OnInput(EPlayerInputType actionType, int diceIndex)
    {
        if (diceIndex < 0 || diceIndex >= diceInventory.Count)
        {
            LogManager.Log($"인벤토리에 {diceIndex}번 인덱스가 없습니다", ELogType.Test, ELogCategory.GameSystem, ELogLevel.Error);
            return;
        }
        PlayerInputResult inputResult = new PlayerInputResult(actionType, diceInventory[diceIndex]);
        inputCallback.Invoke(inputResult);
    }

    public void DiceInventorySetting(List<Dice> diceList)
    {
        diceInventory = diceList;
    }

    public void Dispose()
    {
        inputComponenet.Disable();
    }
}
