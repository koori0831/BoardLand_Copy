using System;

[Serializable]
public class AddDeffendPointSkill : SkillEffectBase
{
    public int AddAmount;
    public ESkillTarget SkillTarget;

    public override void ApplyEffect(SkillContext context, Action endCallback)
    {
        context.GetTargetUnit(SkillTarget).Status.ModifyDeffendPoint(AddAmount);
        base.ApplyEffect(context, endCallback);
    }
}
