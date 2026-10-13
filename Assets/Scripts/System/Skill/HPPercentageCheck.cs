using System;

[Serializable]
public class HPPercentageCheck : ISkillCondition
{
    public int HigherPercentage; // 이상
    public int LowerPercentage; // 이하
    public ESkillTarget SkillTarget;

    public bool IsConditionMet(SkillContext context)
    {
        UnitStatusComponent tartgetStatus = context.GetTargetUnit(SkillTarget).Status;
        float currentPercentage = ((float)tartgetStatus.GetCurrentHealth() / tartgetStatus.GetMaxHealth()) * 100f;
        if (currentPercentage > HigherPercentage && currentPercentage < LowerPercentage)
            return true;
        else
            return false;
    }
}
