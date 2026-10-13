using System;

public interface ISkillCondition
{
    bool IsConditionMet(SkillContext context);
}
public class SkillContext
{
    public Game Game;
    public Tiles Tiles;
    public int LastSkillIndex = -1;
    public SkillContext(Game game, Tiles tiles)
    {
        Game = game;
        Tiles = tiles;
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
