public enum EElementType
{
    None,
    Earth,
    Water,
    Fire,
}
public enum ETileType
{
    Attack,
    Deffend,
    Heal,
    // TreasureBox,
    // 이후에 기믹 요소가 있는 특수 타일이 추가될 수 있음
}

public struct Tile
{
    public EElementType ElementType;
    public ETileType TileType;
    public int TileLevel;

    public Tile(EElementType elementType, ETileType tileType, int tileLevel = 1)
    {
        this.ElementType = elementType;
        this.TileType = tileType;
        this.TileLevel = tileLevel;
    }
}