using Core;
using System.Collections.Generic;

public enum EPlayerInputType
{
    Move,
    Action,
    TurnEnd,
}

public class Board
{
    private static readonly int boardSize = 12;
    private static readonly int playerCount = 2;

    private List<Game> games = new();
    private Tiles tiles;
    private int currentGameIndex;

    public Board(PlayerData playerData, EnemyData enemyData)
    {
        tiles = new Tiles(boardSize);

        for (int i = 0; i < playerCount; i++)
            games.Add(new Game(playerData, enemyData, tiles));
    }

    public void StartGame()
    {
        // 우선 무조건 Player1(0) 부터 턴을 시작한다.
        currentGameIndex = 0;
        StartCurrentTurn();
    }

    public void Dispose()
    {
        foreach (Game game in games)
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
