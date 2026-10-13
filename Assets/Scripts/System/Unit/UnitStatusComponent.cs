using System;

public class UnitStatusComponent
{
    private UnitStatus status;
    private int currentHealth;
    private int currentDeffendPoint;

    public event Action<int, int> HealthChanged;
    public event Action<int> DeffendPointChanged;

    public void Initialize(UnitStatus status)
    {
        this.status = status;
        currentHealth = status.MaxHealth;
        currentDeffendPoint = 0;
    }

    public void ModifyHealth(int value)
    {
        currentHealth += value;
        currentHealth = Math.Clamp(currentHealth, 0, status.MaxHealth);
        HealthChanged?.Invoke(currentHealth, status.MaxHealth);
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return status.MaxHealth;
    }
    public void ModifyDeffendPoint(int value)
    {
        currentDeffendPoint += value;
        currentDeffendPoint = Math.Clamp(currentDeffendPoint, 0, status.MaxDeffendPoint);
        DeffendPointChanged?.Invoke(currentDeffendPoint);
    }
    public int GetCurrentDeffendPoint()
    {
        return currentDeffendPoint;
    }
}