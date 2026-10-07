using System.Collections.Generic;
using UnityEngine;

public struct SkillData
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public List<ISkillCondition> skillConditions;
    public List<SkillEffectBase> skillEffects;
}
