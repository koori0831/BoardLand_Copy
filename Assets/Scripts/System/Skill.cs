using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public List<ISkillCondition> skillConditions;
    public List<SkillEffectBase> skillEffects;
}
