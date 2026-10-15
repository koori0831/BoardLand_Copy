using Alchemy.Inspector;
using Core;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameManagerMono : MonoBehaviour
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
    private TileInfoUI tileInfoUI;

    [SerializeField]
    private MainPopupUI mainPopupUI;

    [SerializeField]
    private GameEndPopupUI gameEndPopupUI;

    [SerializeField]
    private WeaponInfoUI weaponInfoUI;

    [SerializeField]
    private ItemInventoryUI itemInventoryUI;

    [SerializeField]
    private ShopPopupUI shopPopupUI;

    [SerializeField]
    private SerializableDictionary<EElementType, TileLevelTableSO> tileLevelTables;

    [SerializeField]
    private List<WeaponDataSO> weaponDataSOs;

    [SerializeField]
    private List<ItemDataSO> itemDataSOs;

    private Board board;

    private void Start()
    {
        StartGame();
    }

    [LabelText("게임 시작")]
    [Button]
    private void StartGame()
    {
        LogManager.Log($"게임 시작 됨.", ELogType.Test, ELogCategory.GameSystem);
        board?.Dispose();
        List<WeaponData> weaponDatas = CreateWeaponDataList();
        board = new Board(playerDataSO.PlayerData, enemyDataSO.ConvertEnemyData(), CreateTileLevelDataDictionary(), weaponDatas, LocalGameSetting.PlayerCount);
        if (boardRenderer != null)
            boardRenderer.Bind(board);
        if (diceUI != null)
            diceUI.Bind(board);
        if (mainPopupUI != null)
            mainPopupUI.Bind(board);
        if (gameEndPopupUI != null)
            gameEndPopupUI.Bind(board);
        if (weaponInfoUI != null)
            weaponInfoUI.Bind(board);
        if (itemInventoryUI != null)
            itemInventoryUI.Bind(board);
        if (shopPopupUI != null)
            shopPopupUI.Bind(board, CreateItemDataList(), weaponDatas);
        board.StartGame();
        if (tileInfoUI != null)
            tileInfoUI.Bind(board);
    }

    private Dictionary<EElementType, TileLevelData> CreateTileLevelDataDictionary()
    {
        Dictionary<EElementType, TileLevelData> tileLevelDatas = new();
        foreach (var kvp in tileLevelTables)
            tileLevelDatas.TryAdd(kvp.Key, kvp.Value.ConvertTileLevelData());
        return tileLevelDatas;
    }

    private List<WeaponData> CreateWeaponDataList()
    {
        List<WeaponData> weaponDatas = new();
        foreach (WeaponDataSO weaponDataSO in weaponDataSOs)
            weaponDatas.Add(weaponDataSO.ConvertWeaponData());
        return weaponDatas;
    }

    private List<ItemData> CreateItemDataList()
    {
        List<ItemData> itemDatas = new();
        foreach (ItemDataSO itemDataSO in itemDataSOs)
            itemDatas.Add(itemDataSO.ConvertItemData());
        return itemDatas;
    }

    private void OnDestroy()
    {
        board?.Dispose();
    }
}
