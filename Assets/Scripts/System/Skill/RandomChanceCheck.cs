using System;

[Serializable]
public class RandomChanceCheck : ISkillCondition
{
    public int Percentage = 100;

    public bool IsConditionMet(SkillContext context)
    {
        return new Random().Next(0, 100) < Percentage;
    }
}
