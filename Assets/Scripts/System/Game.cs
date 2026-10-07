using Core;
using System;

public class Game
{
    private Player playerUnit;
    private Enemy enemyUnit;

    public Game(PlayerData playerData, EnemyData enemyData)
    {
        playerUnit = new Player(playerData.Status);
        enemyUnit = new Enemy(enemyData.Status);
    }

    public void PlayerAction(EPlayerActionType actionType, Action turnEndCallBack)
    {
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
        turnEndCallBack.Invoke();
    }

    public void StartEnemyTurn(Action turnEndCallBack)
    {
        LogManager.Log($"적의 공격", ELogType.Test, ELogCategory.GameSystem);
        playerUnit.TakeAttack(enemyUnit.Status.GetCurrentAttackDamage());
        turnEndCallBack.Invoke();
    }
}