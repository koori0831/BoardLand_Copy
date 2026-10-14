using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public string WeaponName;
    public string WeaponDescription;

    public List<SkillSO> Skills;

    public WeaponData ConvertWeaponData()
    {
        List<Skill> skillList = new();
        foreach (SkillSO skillSO in Skills)
            skillList.Add(new Skill(skillSO.SkillConditions, skillSO.SkillEffects));

        return new WeaponData(WeaponName, WeaponDescription, skillList);
    }
}
