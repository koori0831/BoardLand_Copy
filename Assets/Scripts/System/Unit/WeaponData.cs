using UnityEngine;

public class WeaponData
{
    public string WeaponName;
    public string WeaponDescription;
    public Sprite WeaponIcon;
    public Skill ActiveSkill;

    public WeaponData(string weaponName, string weaponDescription, Sprite weaponIcon, Skill activeSkill)
    {
        WeaponName = weaponName;
        WeaponDescription = weaponDescription;
        WeaponIcon = weaponIcon;
        ActiveSkill = activeSkill;
    }
}
