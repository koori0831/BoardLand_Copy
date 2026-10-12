using System;
using System.Collections.Generic;

public interface ISkillCondition
{
    bool IsConditionMet(SkillContext context);
}
public class SkillContext
{
    public List<Game> Games;
    public Tiles Tiles;
    public int CurrentGameIndex;
    public SkillContext(List<Game> games, Tiles tiles, int currentGameIndex)
    {
        Games = games;
        Tiles = tiles;
        CurrentGameIndex = currentGameIndex;
    }
}
[Serializable]
public abstract class SkillEffectBase
{
    public virtual void ApplyEffect(SkillContext context, Action endCallback)
    {
        endCallback?.Invoke();
    }
}
