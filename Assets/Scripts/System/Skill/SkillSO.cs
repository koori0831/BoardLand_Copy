using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Objects/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    [field: SerializeReference] public List<ISkillCondition> skillConditions;
    [field: SerializeReference] public List<SkillEffectBase> skillEffects;
}
