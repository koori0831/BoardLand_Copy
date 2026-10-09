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

    public List<Game> Games { get; private set; } = new();
    public Tiles Tiles { get; private set; }
    private int currentGameIndex;

    public Board(PlayerData playerData, EnemyData enemyData)
    {
        Tiles = new Tiles(boardSize);

        for (int i = 0; i < playerCount; i++)
            Games.Add(new Game(playerData, enemyData, Tiles));
    }

    public void StartGame()
    {
        // 우선 무조건 Player1(0) 부터 턴을 시작한다.
        currentGameIndex = 0;
        StartCurrentTurn();
    }

    public void Dispose()
    {
        foreach (Game game in Games)
            game.Dispose();
    }

    private void StartCurrentTurn()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        Games[currentGameIndex].TurnStart(TurnEndCallback);
    }

    private void TurnEndCallback()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 종료 됨.", ELogType.Test, ELogCategory.GameSystem);
        currentGameIndex = (currentGameIndex + 1) % Games.Count;
        StartCurrentTurn();
    }
}
