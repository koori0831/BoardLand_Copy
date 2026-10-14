public class AttackTile : Tile
{
    public AttackTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.Attack;

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Enemy.TakeAttack(actionContext.Dice.DiceNumber);
    }
}