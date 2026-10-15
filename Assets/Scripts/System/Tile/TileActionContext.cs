using System;
using System.Collections.Generic;

public class TileActionContext
{
    public Player Player;
    public Enemy Enemy;
    public Dice Dice;
    public Tiles Tiles;
    public int TileIndex;
    public List<WeaponData> WeaponDatas;
    public Action OpenShop;
    public TileActionContext(Player player, Enemy enemy, Dice dice, Tiles tiles, int tileIndex, List<WeaponData> weaponDatas, Action openShop)
    {
        Player = player;
        Enemy = enemy;
        Dice = dice;
        Tiles = tiles;
        TileIndex = tileIndex;
        WeaponDatas = weaponDatas;
        OpenShop = openShop;
    }
}