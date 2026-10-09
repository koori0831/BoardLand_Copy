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

public abstract class Tile
{
    public EElementType ElementType;
    public ETileType TileType;
    public int TileLevel;

    public Tile(ETileType tileType, EElementType elementType)
    {
        TileType = tileType;
        ElementType = elementType;
        TileLevel = 1;
    }

    public abstract void TileAction(TileActionContext actionContext);
}