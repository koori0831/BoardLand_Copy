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
    TreasureBox,
    Shop,
    // 이후에 기믹 요소가 있는 특수 타일이 추가될 수 있음
}

public abstract class Tile
{
    public TileStatus Status;
    public abstract ETileType TileType { get; }

    public Tile(EElementType elementType, TileLevelData levelData)
    {
        Status = new TileStatus(elementType, levelData);
    }

    public abstract void TileAction(TileActionContext actionContext);
}