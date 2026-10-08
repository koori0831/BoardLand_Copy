using Core;
using System.Collections.Generic;

public enum EPlayerActionType
{
    Move,
    Attack,
    TurnEnd,
}

public class Board
{
    private static readonly int boardSize = 12;
    private static readonly int playerCount = 2;

    private List<Game> games = new();
    private List<Tile> tiles = new();
    private int currentGameIndex;

    public Board(PlayerData playerData, EnemyData enemyData)
    {
        for (int i = 0; i < playerCount; i++)
            games.Add(new Game(playerData, enemyData));

        TileInitialize();
    }

    private void TileInitialize()
    {
        for (int i = 0; i < boardSize; i++)
        {
            EElementType elementType = new RandomEnumValue<EElementType>().GetRandomEnumValue();
            ETileType tileType = new RandomEnumValue<ETileType>().GetRandomEnumValue();
            tiles.Add(new Tile(elementType, tileType));
        }
    }

    public void StartGame()
    {
        // 우선 무조건 Player1(0) 부터 턴을 시작한다.
        currentGameIndex = 0;
        StartCurrentTurn();
    }

    public void Dispose()
    {
        foreach (var game in games)
            game.Dispose();
    }

    private void StartCurrentTurn()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        games[currentGameIndex].TurnStart(TurnEndCallback);
    }

    private void TurnEndCallback()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 종료 됨.", ELogType.Test, ELogCategory.GameSystem);
        currentGameIndex = (currentGameIndex + 1) % games.Count;
        StartCurrentTurn();
    }
}
