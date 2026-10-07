using System;
using System.Collections.Generic;
using System.Text;

public class Player : Unit
{
    public int Position = 0; // currentTileIndex
    // 가지고 있는 주사위(카드) 목록
    public List<DiceCard> CardInventory;
    // 가지고 있는 아이템(무기) 목록(인벤토리)
    public List<Weapon> WeaponInventory;
}
