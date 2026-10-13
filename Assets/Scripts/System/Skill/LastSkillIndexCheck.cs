using System;

[Serializable]
public class LastSkillIndexCheck : ISkillCondition
{
    public int CheckIndex = -1;
    // true인 경우 이전 스킬 인덱스가 같은 경우에만 true.
    // false인 경우 이전 스킬 인덱스가 선택한 인덱스가 아닌 경우에만 true.
    public bool AllowCheckIndex = false;

    public bool IsConditionMet(SkillContext context)
    {
        bool isAllow;
        if (context.LastSkillIndex == CheckIndex)
            isAllow = AllowCheckIndex ? true : false;
        else
            isAllow = AllowCheckIndex ? false : true;

        return isAllow;
    }
}
