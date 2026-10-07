public enum EElementType
{
    None,
    Earth,
    Water,
    Fire,
}
public enum ETileType
{
    None,
    Attack,
    Defend,
    Trap,
    // 이후에 기믹 요소가 있는 특수 타일이 추가될 수 있음
}

public class Tile
{
    public EElementType elementType = EElementType.None;
    public ETileType tileType = ETileType.None;
}