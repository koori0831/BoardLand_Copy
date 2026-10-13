public static class TileFactory
{
    public static Tile Create(ETileType tileType, EElementType elementType)
    {
        return tileType switch {
            ETileType.Attack => new AttackTile(elementType),
            ETileType.Deffend => new DeffendTile(elementType),
            ETileType.Heal => new HealTile(elementType),
            _ => null
        };
    }
}