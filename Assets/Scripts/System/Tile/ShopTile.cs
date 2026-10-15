public class ShopTile : Tile
{
    public ShopTile(EElementType elementType, TileLevelData levelData) : base(elementType, levelData)
    {
    }

    public override ETileType TileType => ETileType.Shop;

    // 주사위를 사용하면 상점이 열린다.
    public override void TileAction(TileActionContext actionContext)
    {
        actionContext.OpenShop?.Invoke();
    }
}
