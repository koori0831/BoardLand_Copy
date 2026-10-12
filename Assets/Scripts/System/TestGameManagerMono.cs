using Alchemy.Inspector;
using Core;
using UnityEngine;

public class TestGameManagerMono : MonoBehaviour
{
    [SerializeField] 
    private PlayerDataSO playerDataSO;
    
    [SerializeField] 
    private EnemyDataSO enemyDataSO;

    [SerializeField]
    private BoardRenderer boardRenderer;

    [SerializeField]
    private DiceDeckUI diceUI;

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
        if (boardRenderer != null)
            boardRenderer.Bind(board);
        if (diceUI != null)
            diceUI.Bind(board.Games);
        board.StartGame();
    }

    private void OnDestroy()
    {
        board?.Dispose();
    }
}
