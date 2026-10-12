public class DeffendTile : Tile
{
    public DeffendTile(EElementType elementType) : base(ETileType.Deffend, elementType)
    {
    }

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Player.Status.ModifyDeffendPoint(actionContext.Dice.DiceNumber);
    }
}