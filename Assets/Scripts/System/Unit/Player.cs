using Core;
using System;
using System.Collections.Generic;

public class Player : Unit
{
    // 가지고 있는 주사위 목록
    private List<Dice> diceInventory = null;
    // 장착 중인 무기
    private Weapon weapon = null;
    // 가지고 있는 아이템 목록 (겹치지 않고 한 칸에 하나씩)
    private List<Item> itemInventory = null;
    private static readonly int itemInventorySize = 3;
    private int gold = 0;

    private UnitInputComponent inputComponenet;
    private Action<PlayerInputResult> inputCallback;

    public Weapon Weapon => weapon;
    public List<Item> ItemInventory => itemInventory;
    public bool IsItemInventoryFull => itemInventory.Count >= itemInventorySize;
    public int Gold => gold;
    public event Action<Weapon> WeaponChanged;
    public event Action<List<Item>> ItemInventoryChanged;
    public event Action<int> GoldChanged;

    public Player(UnitStatus status) : base(status)
    {
        inputComponenet = new UnitInputComponent();
        diceInventory = new List<Dice>();
        itemInventory = new List<Item>();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        GoldChanged?.Invoke(gold);
    }

    public bool UseGold(int amount)
    {
        if (gold < amount)
            return false;
        gold -= amount;
        GoldChanged?.Invoke(gold);
        return true;
    }

    public bool AddItem(Item item)
    {
        if (IsItemInventoryFull)
            return false;
        itemInventory.Add(item);
        ItemInventoryChanged?.Invoke(itemInventory);
        return true;
    }

    // 사용한 아이템은 인벤토리에서 빠진다.
    public void UseItem(int itemIndex, SkillContext context)
    {
        if (itemIndex < 0 || itemIndex >= itemInventory.Count)
            return;
        Item item = itemInventory[itemIndex];
        itemInventory.RemoveAt(itemIndex);
        ItemInventoryChanged?.Invoke(itemInventory);

        if (item.UseSkill == null)
            return;
        LogManager.Log($"아이템 사용 : {item.Data.ItemName}", ELogType.Test, ELogCategory.GameSystem);
        new SkillExecutor(item.UseSkill, context).ExecuteSkill();
    }

    public bool BuyItem(ItemData itemData)
    {
        if (IsItemInventoryFull || UseGold(itemData.Price) == false)
            return false;
        return AddItem(new Item(itemData));
    }

    public bool BuyWeapon(WeaponData weaponData)
    {
        if (UseGold(weaponData.Price) == false)
            return false;
        EquipWeapon(new Weapon(weaponData));
        return true;
    }

    // 새 무기를 얻으면 기존 무기를 버림
    public void EquipWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
        LogManager.Log($"무기 장착 : {weapon.Data.WeaponName}", ELogType.Test, ELogCategory.GameSystem);
        WeaponChanged?.Invoke(weapon);
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
