public class HealTile : Tile
{
    public HealTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.Heal;

    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.Player.TakeHeal(actionContext.Dice.DiceNumber);
    }
}