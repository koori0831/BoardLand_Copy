using System.Collections.Generic;

public class WeaponData
{
    public string WeaponName;
    public string WeaponDescription;
    public List<Skill> Skills;

    public WeaponData(string weaponName, string weaponDescription, List<Skill> skills)
    {
        WeaponName = weaponName;
        WeaponDescription = weaponDescription;
        Skills = skills;
    }
}
