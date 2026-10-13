using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public List<ISkillCondition> skillConditions;
    public List<SkillEffectBase> skillEffects;

    public Skill(string skillName, string skillDescription, Sprite skillIcon, List<ISkillCondition> skillConditions, List<SkillEffectBase> skillEffects)
    {
        this.skillName = skillName;
        this.skillDescription = skillDescription;
        this.skillIcon = skillIcon;
        this.skillConditions = skillConditions;
        this.skillEffects = skillEffects;
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
