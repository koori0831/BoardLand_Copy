public class Weapon
{
    public WeaponData Data { get; private set; }
    public Skill ActiveSkill => Data.ActiveSkill;

    public Weapon(WeaponData data)
    {
        Data = data;
    }
}
