public class DeffendTile : Tile
{
    public DeffendTile(EElementType elementType) : base(elementType)
    {
    }

    public override ETileType TileType => ETileType.Deffend;

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Player.Status.ModifyDeffendPoint(actionContext.Dice.DiceNumber);
    }
}