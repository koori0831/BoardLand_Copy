using System.Collections.Generic;

public class Skill
{
    public List<ISkillCondition> skillConditions;
    public List<SkillEffectBase> skillEffects;

    public Skill(List<ISkillCondition> skillConditions, List<SkillEffectBase> skillEffects)
    {
        this.skillConditions = skillConditions;
        this.skillEffects = skillEffects;
    }
    public Skill(Skill skill)
    {
        skillConditions = new List<ISkillCondition>(skill.skillConditions);
        skillEffects = new List<SkillEffectBase>(skill.skillEffects);
    }

    public bool CanUse(SkillContext context)
    {
        if (skillConditions == null || skillConditions.Count < 0)
            return true;
        foreach (ISkillCondition condition in skillConditions)
        {
            if (condition.IsConditionMet(context) == false)
                return false;
        }
        return true;
    }
}
