using System;

[Serializable]
public class DefaultAttackSkill : SkillEffectBase
{
    public int Damage;
    public ESkillTarget SkillTarget;

    public override void ApplyEffect(SkillContext context, Action endCallback)
    {
        context.GetTargetUnit(SkillTarget).TakeAttack(Damage);
        base.ApplyEffect(context, endCallback);
    }
}
