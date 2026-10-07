using Core;
using System.Collections.Generic;

public class Player : Unit
{
    private int position = 0; // currentTileIndex
    // 가지고 있는 주사위(카드) 목록
    private List<DiceCard> cardInventory = new();
    // 가지고 있는 아이템(무기) 목록
    private List<Weapon> weaponInventory = new();

    public Player(UnitStatus status) : base(status)
    {
    }

    public void Move()
    {
        position += 1;
        LogManager.Log($"플레이어 이동. 현재 위치 : {position}", ELogType.Test, ELogCategory.GameSystem);
    }
}
