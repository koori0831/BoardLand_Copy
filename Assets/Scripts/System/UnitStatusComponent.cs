using System;

public class UnitStatusComponent
{
    private UnitStatus status;
    private int currentHealth;
    private int currentAttackDamage;
    private int currentDeffendPoint;

    public void Initialize(UnitStatus status)
    {
        this.status = status;
        currentHealth = status.MaxHealth;
        currentAttackDamage = status.AttackDamage;
        currentDeffendPoint = 0;
    }

    public void ModifyHealth(int value)
    {
        currentHealth += value;
        currentHealth = Math.Clamp(currentHealth, 0, status.MaxHealth);
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
        currentAttackDamage = Math.Min(currentAttackDamage, 0);
    }
    public int GetCurrentAttackDamage()
    {
        return currentAttackDamage;
    }
    public void ModifyDeffendPoint(int value)
    {
        currentDeffendPoint += value;
        currentDeffendPoint = Math.Clamp(currentDeffendPoint, 0, status.MaxDeffendPoint);
    }
    public int GetCurrentDeffendPoint()
    {
        return currentDeffendPoint;
    }
}