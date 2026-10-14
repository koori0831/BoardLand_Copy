using Core;
using System;
using System.Collections.Generic;

public enum EPlayerInputType
{
    Move,
    Action,
    TurnEnd,
    Cancel,
}

public class Board
{
    private static readonly int boardSize = 12;
    private static readonly int playerCount = 2;

    public List<Game> Games { get; private set; } = new();
    public Tiles Tiles { get; private set; }
    private int currentGameIndex = -1;

    private List<int> defeatGameList = new(); // 적에게 진 게임 리스트
    private List<int> winGameList = new(); // 적에게 이긴 게임 리스트
    private List<int> readerBoard = new(); 

    public int CurrentGameIndex => currentGameIndex;
    public event Action<int> CurrentGameChanged;
    public event Action AllGameEnd;

    public Board(PlayerData playerData, EnemyData enemyData, Dictionary<EElementType, TileLevelData> tileLevelDatas)
    {
        Tiles = new Tiles(boardSize, tileLevelDatas);

        for (int i = 0; i < playerCount; i++)
            Games.Add(new Game(playerData, enemyData, Tiles, OnGameEnd));
    }

    public void StartGame()
    {
        defeatGameList.Clear();
        winGameList.Clear();
        readerBoard.Clear();

        // 우선 무조건 Player1(0) 부터 턴을 시작한다.
        currentGameIndex = 0;
        StartCurrentTurn();
    }

    private void OnGameEnd(bool isPlayerWin)
    {
        if (isPlayerWin)
            winGameList.Add(currentGameIndex);
        else
            defeatGameList.Add(currentGameIndex);

        CheckAllGameEnd();
    }

    private void CheckAllGameEnd()
    {
        int remainGameCount = Games.Count - (defeatGameList.Count + winGameList.Count);
        // 하나만 남았다면 승리 목록으로 이동
        if (remainGameCount == 1)
        {
            for (int i = 0; i < Games.Count; i++)
                if (Games[i].IsGameEnd == true)
                    winGameList.Add(i);
        }
        if (remainGameCount <= 1)
        {
            // 이긴 게임 리스트를 순회하여 인덱스를 넣고 진 게임 리스트를 역방향 순회하여 넣어서 리더보드를 완성 시킨다.
            foreach (int gameIndex in winGameList)
                readerBoard.Add(gameIndex);
            defeatGameList.Reverse();
            foreach (int gameIndex in defeatGameList)
                readerBoard.Add(gameIndex);
            AllGameEnd?.Invoke();
        }
    }

    public void Dispose()
    {
        foreach (Game game in Games)
            game.Dispose();
        CurrentGameChanged = null;
    }

    private void StartCurrentTurn()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        CurrentGameChanged?.Invoke(currentGameIndex);
        Games[currentGameIndex].TurnStart(TurnEndCallback);
    }

    private void TurnEndCallback()
    {
        LogManager.Log($"Player{currentGameIndex + 1}의 턴이 종료 됨.", ELogType.Test, ELogCategory.GameSystem);
        currentGameIndex = (currentGameIndex + 1) % Games.Count;
        StartCurrentTurn();
    }
}
