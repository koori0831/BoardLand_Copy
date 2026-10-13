public class AttackTile : Tile
{
    public AttackTile(EElementType elementType) : base(elementType)
    {
    }

    public override ETileType TileType => ETileType.Attack;

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Enemy.TakeAttack(actionContext.Dice.DiceNumber);
    }
}