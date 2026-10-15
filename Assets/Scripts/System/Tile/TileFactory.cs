public static class TileFactory
{
    public static Tile Create(ETileType tileType, EElementType elementType, TileLevelData levelData)
    {
        return tileType switch {
            ETileType.Attack => new AttackTile(elementType, levelData),
            ETileType.Deffend => new DeffendTile(elementType, levelData),
            ETileType.Heal => new HealTile(elementType, levelData),
            ETileType.TreasureBox => new TreasureBoxTile(elementType, levelData),
            ETileType.Shop => new ShopTile(elementType, levelData),
            _ => null
        };
    }
}