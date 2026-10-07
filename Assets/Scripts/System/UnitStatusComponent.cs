using UnityEngine;

public class UnitStatusComponent
{
    private UnitStatus status;
    private int currentHealth;
    private int currentAttackDamage;

    public void Initialize(UnitStatus status)
    {
        this.status = status;
        currentHealth = status.MaxHealth;
        currentAttackDamage = status.AttackDamage;
    }

    public void ModifyHealth(int value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0, status.MaxHealth);
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return status.MaxHealth;
    }
    public void ModifyAttackDamage(int value)
    {
        currentAttackDamage += value;
        currentAttackDamage = Mathf.Min(currentAttackDamage, 0);
    }
    public int GetCurrentAttackDamage()
    {
        return currentAttackDamage;
    }
}