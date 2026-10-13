using Core;

public abstract class Unit
{
    public UnitStatusComponent Status { get; private set; }

    public Unit(UnitStatus status)
    {
        this.Status = new UnitStatusComponent();
        Status.Initialize(status);
    }

    public void TakeAttack(int damage)
    {
        int finalDamage = damage - Status.GetCurrentDeffendPoint();
        Status.ModifyDeffendPoint(-damage);
        if (finalDamage > 0)
        {
            Status.ModifyHealth(-finalDamage);
            LogManager.Log($"피해 받음. 대미지 : {finalDamage}, 현재 체력 : {Status.GetCurrentHealth()}/{Status.GetMaxHealth()}", ELogType.Test, ELogCategory.GameSystem);
        }
        else
        {
            LogManager.Log($"방어함. 현재 방어 포인트 : {Status.GetCurrentDeffendPoint()}", ELogType.Test, ELogCategory.GameSystem);
        }
    }
    public void TakeHeal(int value)
    {
        Status.ModifyHealth(value);
        LogManager.Log($"회복. 현재 체력 : {Status.GetCurrentHealth()}/{Status.GetMaxHealth()}", ELogType.Test, ELogCategory.GameSystem);
    }
}
