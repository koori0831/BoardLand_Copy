using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Objects/Skill")]
public class SkillSO : ScriptableObject
{
    public string SkillName;
    public string SkillDescription;
    public Sprite SkillIcon;
    [field: SerializeReference] public List<ISkillCondition> SkillConditions;
    [field: SerializeReference] public List<SkillEffectBase> SkillEffects;
}
