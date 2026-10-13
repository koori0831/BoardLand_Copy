using System;

[Serializable]
public class HealSkill : SkillEffectBase
{
    public int HealAmount;
    public ESkillTarget SkillTarget;

    public override void ApplyEffect(SkillContext context, Action endCallback)
    {
        context.GetTargetUnit(SkillTarget).TakeHeal(HealAmount);
        base.ApplyEffect(context, endCallback);
    }
}
