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
    private Dictionary<EPlayerInputType, Action<Dice>> playerInputActionDictionary = null;
    private static readonly int diceInventorySize = 5;

    public Player Player => playerUnit;
    public Enemy Enemy => enemyUnit;
    public int PlayerPosition => playerPosition;

    public event Action<int, int> PlayerMoved;
    public event Action<List<Dice>> DiceRolled;

    public Game(PlayerData playerData, EnemyData enemyData, Tiles tiles)
    {
        playerUnit = new Player(playerData.Status);
        enemyUnit = new Enemy(enemyData.Status, enemyData.Skills);
        this.tiles = tiles;

        playerInputActionDictionary = new Dictionary<EPlayerInputType, Action<Dice>>()
        {
            [EPlayerInputType.Move] = PlayerMove,
            [EPlayerInputType.Action] = PlayerTileAction,
            [EPlayerInputType.TurnEnd] = PlayerTurnEnd,
        };
    }

    public void TurnStart(Action turnEndCallBack)
    {
        this.turnEndCallBack = turnEndCallBack;

        RollDice();
        GameCallbackChain();
    }

    private void GameCallbackChain()
    {
        playerUnit.InputReady(inputResult =>
        {
            OnPlayerInput(inputResult, () =>
            {
                if (inputResult.ActionType == EPlayerInputType.TurnEnd)
                    EndTurn();
                else
                    EnemyPhase(GameCallbackChain);
            });
        });
    }

    public void Dispose()
    {
        playerUnit.Dispose();
        turnEndCallBack = null;
        PlayerMoved = null;
    }

    private void OnPlayerInput(PlayerInputResult inputResult, Action moveNext)
    {
        LogManager.Log($"플레이어 행동({inputResult.ActionType}).", ELogType.Test, ELogCategory.GameSystem);
        if (playerInputActionDictionary.TryGetValue(inputResult.ActionType, out Action<Dice> playerAction))
        {
            playerAction.Invoke(inputResult.Dice);
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
        DiceRolled.Invoke(diceList);
    }

    private void PlayerMove(Dice dice)
    {
        int from = playerPosition;
        playerPosition = (playerPosition + dice.DiceNumber) % tiles.TileList.Count;
        PlayerMoved?.Invoke(from, dice.DiceNumber);
        LogManager.Log($"플레이어 {dice.DiceNumber}만큼 이동. 현재 타일 타입: {tiles.TileList[playerPosition].GetType()}", ELogType.Test, ELogCategory.GameSystem);
    }
    private void PlayerTileAction(Dice dice)
    {
        TileActionContext actionContext = new TileActionContext(playerUnit, enemyUnit, dice);
        tiles.TileList[playerPosition].TileAction(actionContext);
    }
    private void PlayerTurnEnd(Dice dice)
    {
    }

    private void EnemyPhase(Action moveNext)
    {
        LogManager.Log($"적의 공격", ELogType.Test, ELogCategory.GameSystem);
        SkillContext context = new(enemyUnit, playerUnit, tiles);
        SkillExecuter.ExecuteSkill(enemyUnit.GetNextSkill(context), context);
        LogManager.Log($"");
        moveNext?.Invoke();
    }

    private void EndTurn()
    {
        turnEndCallBack?.Invoke();
    }
}
