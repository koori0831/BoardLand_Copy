using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public record SkillData
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public List<ISkillCondition> skillConditions;
    public List<SkillEffectBase> skillEffects;
}
