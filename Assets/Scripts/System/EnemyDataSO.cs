using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public EnemyData EnemyData;

    //public List<Skill> GetSkillList()
    //{
    //    List<Skill> results = new();
    //    foreach (SkillSO skillSO in skills)
    //        results.Add(new Skill(skillSO.skillName, skillSO.skillDescription, skillSO.skillIcon, skillSO.skillConditions, skillSO.skillEffects));

    //    return results;
    //}
}
