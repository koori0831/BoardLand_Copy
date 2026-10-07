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

    private Game player1Game;
    private Game player2Game;
    private List<Tile> tiles = new();
    private EUnitType currentTurnUnit;

    public Board(PlayerData playerData, EnemyData enemyData)
    {
        player1Game = new Game(playerData, enemyData);
        player2Game = new Game(playerData, enemyData);
        
        TileInitialize();
        
        // 우선 무조건 Player1 부터 턴을 시작한다.
        currentTurnUnit = EUnitType.Player1;
        LogManager.Log($"{currentTurnUnit}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
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

    public void PlayerInput(EPlayerActionType actionType)
    {
        if (IsPlayerTurn() == false)
        {
            LogManager.Log("플레이어 턴이 아닙니다.", ELogType.Test, ELogCategory.GameSystem, ELogMessageType.Warning);
            return;
        }

        LogManager.Log($"{currentTurnUnit}이 행동({actionType}) 시도.", ELogType.Test, ELogCategory.GameSystem);

        if (currentTurnUnit == EUnitType.Player1)
            player1Game.PlayerAction(actionType, TurnEndCallback);
        else
            player2Game.PlayerAction(actionType, TurnEndCallback);
    }

    private void StartEnemyTurn()
    {
        if (IsPlayerTurn())
        {
            LogManager.Log("적의 턴이 아닙니다.", ELogType.Test, ELogCategory.GameSystem, ELogMessageType.Warning);
            return;
        }

        if (currentTurnUnit == EUnitType.Enemy1)
            player1Game.StartEnemyTurn(TurnEndCallback);
        else
            player2Game.StartEnemyTurn(TurnEndCallback);
    }

    private void TurnEndCallback()
    {
        LogManager.Log($"{currentTurnUnit}의 턴이 종료 됨.", ELogType.Test, ELogCategory.GameSystem);
        currentTurnUnit = GetNextTurnUnitType();
        LogManager.Log($"{currentTurnUnit}의 턴이 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        if (IsPlayerTurn() == false)
            StartEnemyTurn();
    }

    private EUnitType GetNextTurnUnitType()
    {
        return currentTurnUnit switch
        {
            EUnitType.Player1 => EUnitType.Enemy1,
            EUnitType.Player2 => EUnitType.Enemy2,
            EUnitType.Enemy1 => EUnitType.Player2,
            EUnitType.Enemy2 => EUnitType.Player1,
            _ => EUnitType.Player1,
        };
    }
    private bool IsPlayerTurn()
    {
        if (currentTurnUnit == EUnitType.Player1 || currentTurnUnit == EUnitType.Player2)
            return true;
        else
            return false;
    }
}
