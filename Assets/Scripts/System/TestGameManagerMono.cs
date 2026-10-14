using Alchemy.Inspector;
using Core;
using System.Collections.Generic;
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

    [SerializeField]
    private SerializableDictionary<EElementType, TileLevelTableSO> tileLevelTables;

    private Board board;

    [LabelText("게임 시작")]
    [Button]
    private void StartGame()
    {
        LogManager.Log($"게임 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        board?.Dispose();
        board = new Board(playerDataSO.PlayerData, enemyDataSO.ConvertEnemyData(), CreateTileLevelDataDictionary());
        if (boardRenderer != null)
            boardRenderer.Bind(board);
        if (diceUI != null)
            diceUI.Bind(board.Games);
        board.StartGame();
    }

    private Dictionary<EElementType, TileLevelData> CreateTileLevelDataDictionary()
    {
        Dictionary<EElementType, TileLevelData> tileLevelDatas = new();
        foreach (var kvp in tileLevelTables)
            tileLevelDatas.TryAdd(kvp.Key, kvp.Value.ConvertTileLevelData());
        return tileLevelDatas;
    }

    private void OnDestroy()
    {
        board?.Dispose();
    }
}
