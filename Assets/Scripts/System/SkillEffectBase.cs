using System;
using System.Collections.Generic;
using System.Text;

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
