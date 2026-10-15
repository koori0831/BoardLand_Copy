using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class WeaponDataSO : ScriptableObject
{
    public string WeaponName;
    public string WeaponDescription;
    public Sprite WeaponIcon;
    public int Price;

    public SkillSO ActiveSkill;

    public WeaponData ConvertWeaponData()
    {
        Skill activeSkill = null;
        if (ActiveSkill != null)
            activeSkill = new Skill(ActiveSkill.SkillConditions, ActiveSkill.SkillEffects);

        return new WeaponData(WeaponName, WeaponDescription, WeaponIcon, Price, activeSkill);
    }
}
