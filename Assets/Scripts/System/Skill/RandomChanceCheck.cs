using System;

// 설정한 확률(%)로만 스킬을 사용할 수 있다.
[Serializable]
public class RandomChanceCheck : ISkillCondition
{
    public int Percentage = 100;

    public bool IsConditionMet(SkillContext context)
    {
        return new Random().Next(0, 100) < Percentage;
    }
}
