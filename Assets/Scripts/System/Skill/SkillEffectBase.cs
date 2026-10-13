using System;

public interface ISkillCondition
{
    bool IsConditionMet(SkillContext context);
}

[Serializable]
public abstract class SkillEffectBase
{
    public virtual void ApplyEffect(SkillContext context, Action endCallback)
    {
        endCallback?.Invoke();
    }
}
