public class HealTile : Tile
{
    public HealTile(EElementType elementType) : base(ETileType.Heal, elementType)
    {
    }

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Player.TakeHeal(actionContext.Dice.DiceNumber);
    }
}