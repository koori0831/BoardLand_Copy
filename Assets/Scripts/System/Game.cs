using Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public class Game
{
    private Player playerUnit;
    private Enemy enemyUnit;
    private int playerPosition = 0;
    private int turnCount = 0;
    private int lapCount = 0;
    private Tiles tiles;
    private List<WeaponData> weaponDatas;
    private Dictionary<EPlayerInputType, Action<Dice>> playerInputActionDictionary = null;

    public Player Player => playerUnit;
    public Enemy Enemy => enemyUnit;
    public int PlayerPosition => playerPosition;
    public int TurnCount => turnCount;
    public int LapCount => lapCount;

    public event Action<int, int> PlayerMoved;
    public event Action<int> TurnCountChanged;
    public event Action<int> LapCountChanged;
    public event Action ShopOpened;
    public event Action<List<Dice>> DiceRolled;

    public bool IsGameEnd = false;
    private Action<bool> gameEndCallback;

    public Game(PlayerData playerData, EnemyData enemyData, Tiles tiles, List<WeaponData> weaponDatas, Action<bool> gameEndCallback)
    {
        playerUnit = new Player(playerData.Status);
        enemyUnit = new Enemy(enemyData.Status, enemyData.Skills);
        this.tiles = tiles;
        this.weaponDatas = weaponDatas;
        this.gameEndCallback = gameEndCallback;
        playerInputActionDictionary = new Dictionary<EPlayerInputType, Action<Dice>>()
        {
            [EPlayerInputType.Move] = PlayerMove,
            [EPlayerInputType.Action] = PlayerTileAction,
            [EPlayerInputType.Weapon] = PlayerWeaponSkill,
            [EPlayerInputType.TurnEnd] = PlayerTurnEnd,
        };
    }

    public async UniTask TurnAsync()
    {
        if (IsGameEnd == true)
            return;
        turnCount++;
        TurnCountChanged?.Invoke(turnCount);

        RollDice();
        while (true)
        {
            PlayerInputResult inputResult = await playerUnit.InputReady();
            PlayerPhase(inputResult);
            if (GameEndCheck() == true || inputResult.ActionType == EPlayerInputType.TurnEnd)
                return;

            EnemyPhase();
            if (GameEndCheck() == true)
                return;
        }
    }

    public void Dispose()
    {
        playerUnit.Dispose();
        PlayerMoved = null;
        TurnCountChanged = null;
        LapCountChanged = null;
        ShopOpened = null;
    }

    private void PlayerPhase(PlayerInputResult inputResult)
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
    }

    private void RollDice()
    {
        List<Dice> diceList = new List<Dice>();
        for (int i = 0; i < GameConstant.DiceInventorySize; i++)
        {
            int diceNumber = new Random().Next(1, 7);
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            diceList.Add(new Dice(diceNumber, elementType));
        }
        playerUnit.DiceInventorySetting(diceList);
        DiceRolled?.Invoke(diceList);
    }

    private void PlayerMove(Dice dice)
    {
        int from = playerPosition;
        if (playerPosition + dice.DiceNumber >= tiles.TileList.Count)
        {
            lapCount++;
            LapCountChanged?.Invoke(lapCount);
            playerUnit.AddGold(GameConstant.LapRewardGold);
            LogManager.Log($"{lapCount}바퀴 완료. {GameConstant.LapRewardGold}골드 획득", ELogType.Test, ELogCategory.GameSystem);
        }
        playerPosition = (playerPosition + dice.DiceNumber) % tiles.TileList.Count;
        PlayerMoved?.Invoke(from, dice.DiceNumber);
        LogManager.Log($"플레이어 {dice.DiceNumber}만큼 이동. 현재 타일 타입: {tiles.TileList[playerPosition].GetType()}", ELogType.Test, ELogCategory.GameSystem);
    }
    private void PlayerTileAction(Dice dice)
    {
        // 타일 행동 중에 타일이 교체될 수 있으므로 행동한 타일을 기억해둔다
        Tile tile = tiles.TileList[playerPosition];
        TileActionContext actionContext = new TileActionContext(playerUnit, enemyUnit, dice, tiles, playerPosition, weaponDatas, () => ShopOpened?.Invoke());
        tile.TileAction(actionContext);

        if (tile.Status.ElementType == dice.ElementType)
            tile.Status.AddEXP(dice.DiceNumber);
        Skill tileSkill = tile.Status.GetTileSkill();
        if (tileSkill != null)
            new SkillExecutor(tileSkill, new SkillContext(playerUnit, enemyUnit, tiles)).ExecuteSkill();
    }
    private void PlayerWeaponSkill(Dice dice)
    {
        if (playerUnit.Weapon == null || playerUnit.Weapon.ActiveSkill == null)
        {
            LogManager.Log($"사용할 수 있는 무기 스킬이 없음.", ELogType.Test, ELogCategory.GameSystem, ELogLevel.Warning);
            return;
        }
        LogManager.Log($"무기 스킬 사용 : {playerUnit.Weapon.Data.WeaponName}", ELogType.Test, ELogCategory.GameSystem);
        new SkillExecutor(playerUnit.Weapon.ActiveSkill, new SkillContext(playerUnit, enemyUnit, tiles)).ExecuteSkill();
    }
    public void UseItem(int itemIndex)
    {
        playerUnit.UseItem(itemIndex, new SkillContext(playerUnit, enemyUnit, tiles));
    }

    private void PlayerTurnEnd(Dice dice)
    {
    }

    private void EnemyPhase()
    {
        LogManager.Log($"적의 공격", ELogType.Test, ELogCategory.GameSystem);
        SkillContext context = new(enemyUnit, playerUnit, tiles);
        new SkillExecutor(enemyUnit.GetNextSkill(context), context).ExecuteSkill();
    }

    private bool GameEndCheck()
    {
        if (playerUnit.Status.GetCurrentHealth() <= 0 || enemyUnit.Status.GetCurrentHealth() <= 0)
        {
            IsGameEnd = true;
            // 게임이 끝난 플레이어는 더 이상 입력을 받지 않는다.
            playerUnit.Dispose();
            bool isPlayerWin = enemyUnit.Status.GetCurrentHealth() <= 0;
            gameEndCallback?.Invoke(isPlayerWin);
            return true;
        }
        return false;
    }
}
