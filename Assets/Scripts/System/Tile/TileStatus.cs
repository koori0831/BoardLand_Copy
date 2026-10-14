using System;

public class TileStatus
{
    public EElementType ElementType { get; }
    public TileLevelData LevelData { get; }
    public int CurrentLevel { get; private set; }
    public int CurrentEXP { get; private set; }

    public event Action<int, bool> LevelChanged; // 레벨, isMaxLevel
    public event Action<float> EXPChanged; // 현재 EXP 진행도%

    public TileStatus(EElementType elementType, TileLevelData levelData)
    {
        ElementType = elementType;
        LevelData = levelData;
    }

    public void AddEXP(int amount)
    {
        CurrentEXP += amount;
        CurrentEXP = Math.Clamp(CurrentEXP, 0, LevelData.LevelSteps[^1].RequiredEXP);
        RefreshLevel();
        float percentage = 0f;
        if (CurrentLevel == LevelData.LevelSteps.Count) // 레벨이 맥스인 경우
            percentage = 100f;
        else
        {
            float current = CurrentEXP - LevelData.LevelSteps[CurrentLevel - 1].RequiredEXP;
            float next = LevelData.LevelSteps[CurrentLevel].RequiredEXP - LevelData.LevelSteps[CurrentLevel - 1].RequiredEXP;
            percentage = (current / next) * 100f;
        }

        EXPChanged?.Invoke(percentage);
    }

    public Skill GetTileSkill()
    {
        RefreshLevel();
        return LevelData.LevelSteps[CurrentLevel - 1].Skill;
    }

    private void RefreshLevel()
    {
        CurrentLevel = 0;
        foreach (TileLevelStep levelStep in LevelData.LevelSteps)
        {
            if (CurrentEXP >= levelStep.RequiredEXP)
                CurrentLevel++;
        }
        bool isMaxLevel = CurrentLevel == LevelData.LevelSteps.Count;
        LevelChanged?.Invoke(CurrentLevel, isMaxLevel);
    }
}
