using System.Collections.Generic;
using UnityEngine;

public interface ISkillCondition
{
    bool IsConditionMet(SkillContext context);
}

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
}
