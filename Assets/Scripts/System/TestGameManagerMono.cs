using Alchemy.Inspector;
using Core;
using UnityEngine;

public class TestGameManagerMono : MonoBehaviour
{
    [SerializeField] 
    private PlayerDataSO playerDataSO;
    
    [SerializeField] 
    private EnemyDataSO enemyDataSO;

    private Board board;

    [LabelText("게임 시작")]
    [Button]
    private void StartGame()
    {
        //Enemy enemy = new Enemy(enemyDataSO.bossName, enemyDataSO.bossDescription, enemyDataSO.GetSkillList());
        //Enemy otherEnemy = new Enemy(enemyDataSO.bossName, enemyDataSO.bossDescription, enemyDataSO.GetSkillList());
        //Player player = new Player();
        //Player otherPlayer = new Player();
        //Game user = new Game(player, enemy);
        //Game otherUser = new Game(otherPlayer, otherEnemy);
        LogManager.Log($"게임 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        board = new Board(playerDataSO.PlayerData, enemyDataSO.EnemyData);
    }

    [LabelText("이동하기")]
    [Button]
    private void PlayerInputMove()
    {
        board.PlayerInput(EPlayerActionType.Move);
    }
    [LabelText("공격하기")]
    [Button]
    private void PlayerInputAttack()
    {
        board.PlayerInput(EPlayerActionType.Attack);
    }
    [LabelText("턴 종료하기")]
    [Button]
    private void PlayerInputTurnEnd()
    {
        board.PlayerInput(EPlayerActionType.TurnEnd);
    }
}
