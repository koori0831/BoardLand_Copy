using System;

public class TreasureBoxTile : Tile
{
    public override ETileType TileType => ETileType.TreasureBox;
    public TreasureBoxTile(EElementType elementType) : base(elementType)
    {
    }

    public override void TileAction(TileActionContext actionContext)
    {
        // 성공한 경우
        if (new Random().Next(1, 7) <= actionContext.Dice.DiceNumber)
        {
            // 랜덤한 무기를 뽑아서 플레이어에게 준다.
        }
        // 실패한 경우
        else
        {
            // 미믹에게 공격받는다.
        }
    }
}
