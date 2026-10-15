public class ShopTile : Tile
{
    public ShopTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.Shop;

    // 주사위를 사용하면 상점이 열리고, 사용한 상점은 상점이 아닌 랜덤한 타일로 바뀐다.
    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.OpenShop?.Invoke();
        actionContext.Tiles.ReplaceToRandomTile(actionContext.TileIndex, ETileType.Shop);
    }
}
