using Core;
using System;

public class Game
{
    private Player playerUnit;
    private Enemy enemyUnit;
    private Action turnEndCallBack;

    public Game(PlayerData playerData, EnemyData enemyData)
    {
        playerUnit = new Player(playerData.Status);
        enemyUnit = new Enemy(enemyData.Status);
    }

    public void TurnStart(Action turnEndCallBack)
    {
        this.turnEndCallBack = turnEndCallBack;
        playerUnit.InputReady(OnPlayerInput);
    }

    public void Dispose()
    {
        playerUnit.Dispose();
        turnEndCallBack = null;
    }

    private void OnPlayerInput(EPlayerActionType actionType)
    {
        LogManager.Log($"플레이어 행동({actionType}).", ELogType.Test, ELogCategory.GameSystem);

        switch (actionType)
        {
            case EPlayerActionType.Move:
                playerUnit.Move();
                break;
            case EPlayerActionType.Attack:
                enemyUnit.TakeAttack(playerUnit.Status.GetCurrentAttackDamage());
                break;
            case EPlayerActionType.TurnEnd:
                break;
        }

        EnemyPhase();
    }

    private void EnemyPhase()
    {
        LogManager.Log($"적의 공격", ELogType.Test, ELogCategory.GameSystem);
        playerUnit.TakeAttack(enemyUnit.Status.GetCurrentAttackDamage());
        EndTurn();
    }

    private void EndTurn()
    {
        Action callback = turnEndCallBack;
        turnEndCallBack = null;
        callback?.Invoke();
    }
}
