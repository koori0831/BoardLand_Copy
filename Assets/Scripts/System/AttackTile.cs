public class AttackTile : Tile
{
    public AttackTile(EElementType elementType) : base(ETileType.Attack, elementType)
    {
    }

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Enemy.TakeAttack(actionContext.Dice.DiceNumber);
    }
}