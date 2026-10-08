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
        board?.Dispose();
        board = new Board(playerDataSO.PlayerData, enemyDataSO.EnemyData);
        board.StartGame();
    }

    [LabelText("이동하기")]
    [Button]
    private void PlayerInputMove() => PublishInput(EPlayerActionType.Move);
    [LabelText("공격하기")]
    [Button]
    private void PlayerInputAttack() => PublishInput(EPlayerActionType.Attack);
    [LabelText("턴 종료하기")]
    [Button]
    private void PlayerInputTurnEnd() => PublishInput(EPlayerActionType.TurnEnd);

    private void PublishInput(EPlayerActionType actionType)
    {
        EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(actionType));
    }

    private void OnDestroy()
    {
        board?.Dispose();
    }
}
