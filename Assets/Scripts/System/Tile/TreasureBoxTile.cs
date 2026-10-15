using System;

public class TreasureBoxTile : Tile
{
    public TreasureBoxTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.TreasureBox;
    public override void TileAction(TileActionContext actionContext)
    {
        // 성공한 경우
        if (new Random().Next(1, 7) <= actionContext.Dice.DiceNumber)
        {
            // 랜덤한 무기를 뽑아서 플레이어에게 준다.
            if (actionContext.WeaponDatas != null && actionContext.WeaponDatas.Count > 0)
            {
                WeaponData weaponData = actionContext.WeaponDatas[new Random().Next(0, actionContext.WeaponDatas.Count)];
                actionContext.Player.EquipWeapon(new Weapon(weaponData));
            }
        }
        // 실패한 경우
        else
        {
            // 미믹에게 공격받는다.
            actionContext.Player.TakeAttack(GameConstant.MimicDamage);
        }

        // 사용한 보물상자는 보물상자가 아닌 랜덤한 타일로 바뀐다.
        actionContext.Tiles.ReplaceToRandomTile(actionContext.TileIndex, ETileType.TreasureBox);
    }
}
