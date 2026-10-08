using Core;
using System;
using System.Collections.Generic;

public class Game
{
    private Player playerUnit;
    private Enemy enemyUnit;
    private Action turnEndCallBack;
    private int playerPosition = 0;
    private Tiles tiles;
    private Dictionary<EPlayerInputType, Action<int>> playerInputActionDictionary = null;
    private Dictionary<ETileType, Action<int>> playerTileActionDictionary = null;
    private static readonly int diceInventorySize = 5;

    public Game(PlayerData playerData, EnemyData enemyData, Tiles tiles)
    {
        playerUnit = new Player(playerData.Status);
        enemyUnit = new Enemy(enemyData.Status);
        this.tiles = tiles;

        playerInputActionDictionary = new Dictionary<EPlayerInputType, Action<int>>()
        {
            [EPlayerInputType.Move] = PlayerMove,
            [EPlayerInputType.Action] = PlayerTileAction,
            [EPlayerInputType.TurnEnd] = PlayerTurnEnd,
        };
        playerTileActionDictionary = new Dictionary<ETileType, Action<int>>()
        {
            [ETileType.Attack] = PlayerAttack,
            [ETileType.Deffend] = PlayerDffend,
            [ETileType.Heal] = PlayerHeal,
        };
    }

    public void TurnStart(Action turnEndCallBack)
    {
        this.turnEndCallBack = turnEndCallBack;

        RollDice();
        playerUnit.InputReady(inputResult =>
        {
            OnPlayerInput(inputResult, () =>
            {
                EnemyPhase(() =>
                {
                    EndTurn();
                });
            });
        });
    }

    public void Dispose()
    {
        playerUnit.Dispose();
        turnEndCallBack = null;
    }

    private void OnPlayerInput(PlayerInputResult inputResult, Action moveNext)
    {
        LogManager.Log($"플레이어 행동({inputResult.ActionType}).", ELogType.Test, ELogCategory.GameSystem);
        if (playerInputActionDictionary.TryGetValue(inputResult.ActionType, out Action<int> playerAction))
        {
            playerAction.Invoke(inputResult.Dice.DiceNumder);
        }
        else
        {
            LogManager.Log($"처리할 수 없는 플레이어 행동({inputResult.ActionType}).", ELogType.Test, ELogCategory.GameSystem, ELogLevel.Error);
        }

        moveNext?.Invoke();
    }

    private void RollDice()
    {
        List<Dice> diceList = new List<Dice>();
        for (int i = 0; i < diceInventorySize; i++)
        {
            int diceNumber = new Random().Next(1, 7);
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            diceList.Add(new Dice(diceNumber, elementType));
        }
        playerUnit.DiceInventorySetting(diceList);
    }

    private void PlayerMove(int diceNumber)
    {
        playerPosition = (playerPosition + diceNumber) % tiles.TileList.Count;
        LogManager.Log($"플레이어 {diceNumber}만큼 이동. 현재 타일 타입: {tiles.TileList[playerPosition].TileType}", ELogType.Test, ELogCategory.GameSystem);
    }
    private void PlayerTileAction(int diceNumber)
    {
        ETileType tileType = tiles.TileList[playerPosition].TileType;
        if (playerTileActionDictionary.TryGetValue(tileType, out Action<int> tileAction))
        {
            tileAction.Invoke(diceNumber);
        }
        else
        {
            LogManager.Log($"처리할 수 없는 타일({tileType}).", ELogType.Test, ELogCategory.GameSystem, ELogLevel.Error);
        }
    }
    private void PlayerTurnEnd(int diceNumber)
    {
    }

    private void PlayerAttack(int diceNumber)
    {
        enemyUnit.TakeAttack(diceNumber);
    }
    private void PlayerDffend(int diceNumber)
    {
        playerUnit.Status.ModifyDeffendPoint(diceNumber);
    }
    private void PlayerHeal(int diceNumber)
    {
        playerUnit.TakeHeal(diceNumber);
    }

    private void EnemyPhase(Action moveNext)
    {
        LogManager.Log($"적의 공격", ELogType.Test, ELogCategory.GameSystem);
        playerUnit.TakeAttack(enemyUnit.Status.GetCurrentAttackDamage());
        moveNext?.Invoke();
    }

    private void EndTurn()
    {
        turnEndCallBack?.Invoke();
    }
}
