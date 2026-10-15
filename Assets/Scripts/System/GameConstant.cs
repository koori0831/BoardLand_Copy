public static class GameConstant
{
    // 보드
    public static readonly int BoardSize = 12;

    // 인원
    public static readonly int MinPlayerCount = 2;
    public static readonly int MaxPlayerCount = 4;

    // 플레이어
    public static readonly int DiceInventorySize = 5;
    public static readonly int ItemInventorySize = 3;
    public static readonly int TileActionLimitPerTurn = 1; // 같은 타일에서 한 턴에 행동할 수 있는 횟수

    // 보상과 피해
    public static readonly int LapRewardGold = 15;
    public static readonly int MimicDamage = 5;
}
