public class DeffendTile : Tile
{
    public DeffendTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.Deffend;

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Player.Status.ModifyDeffendPoint(actionContext.Dice.DiceNumber);
    }
}