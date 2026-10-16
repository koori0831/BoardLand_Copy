using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

public enum EPlayerInputType
{
    Move,
    Action,
    TurnEnd,
    Cancel,
    Weapon,
}

public class Board
{
    public List<Game> Games { get; private set; } = new();
    public Tiles Tiles { get; private set; }
    private int currentGameIndex = -1;

    private List<int> defeatGameList = new(); // 적에게 진 게임 리스트
    private List<int> winGameList = new(); // 적에게 이긴 게임 리스트
    private List<int> readerBoard = new();
    private bool isAllGameEnd = false;

    public int CurrentGameIndex => currentGameIndex;
    public List<int> ReaderBoard => readerBoard;
    public event Action<int> CurrentGameChanged;
    public event Action AllGameEnd;

    public Board(PlayerData playerData, EnemyData enemyData, Dictionary<EElementType, TileLevelData> tileLevelDatas, List<WeaponData> weaponDatas, int playerCount)
    {
        Tiles = new Tiles(GameConstant.BoardSize, tileLevelDatas);

        for (int i = 0; i < playerCount; i++)
            Games.Add(new Game(playerData, enemyData, Tiles, weaponDatas));
    }

    public async UniTask StartGame()
    {
        defeatGameList.Clear();
        winGameList.Clear();
        readerBoard.Clear();
        isAllGameEnd = false;

        // 우선 무조건 Player1(0) 부터 턴을 시작한다.
        currentGameIndex = 0;
        // 모든 게임이 끝날 때까지 끝나지 않은 게임의 턴을 순서대로 진행한다.
        while (isAllGameEnd == false)
        {
            if (Games[currentGameIndex].IsGameEnd == false)
            {
                LogManager.Log($"Player{currentGameIndex + 1}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
                CurrentGameChanged?.Invoke(currentGameIndex);
                Game.GameEndCheckData gameEndCheck = await Games[currentGameIndex].TurnAsync();
                LogManager.Log($"Player{currentGameIndex + 1}의 턴이 종료 됨.", ELogType.Test, ELogCategory.GameSystem);
                if (gameEndCheck.IsGameEnd == true)
                    OnGameEnd(gameEndCheck.IsPlayerWin);
            }
            if (isAllGameEnd == false)
                currentGameIndex = (currentGameIndex + 1) % Games.Count;
        }
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
                if (Games[i].IsGameEnd == false)
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
            isAllGameEnd = true;
            AllGameEnd?.Invoke();
        }
    }

    public void Dispose()
    {
        foreach (Game game in Games)
            game.Dispose();
        CurrentGameChanged = null;
        AllGameEnd = null;
    }
}
