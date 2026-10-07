using System;

public struct SkillContext
{
}
[Serializable]
public abstract class SkillEffectBase
{
    public virtual void ApplyEffect(SkillContext context, Action endCallback)
    {
        endCallback?.Invoke();
    }
}
