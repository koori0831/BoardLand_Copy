using UnityEngine;

public class WeaponData
{
    public string WeaponName;
    public string WeaponDescription;
    public Sprite WeaponIcon;
    public int Price;
    public Skill ActiveSkill;

    public WeaponData(string weaponName, string weaponDescription, Sprite weaponIcon, int price, Skill activeSkill)
    {
        WeaponName = weaponName;
        WeaponDescription = weaponDescription;
        WeaponIcon = weaponIcon;
        Price = price;
        ActiveSkill = activeSkill;
    }
}
