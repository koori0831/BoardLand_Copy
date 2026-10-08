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
        Status.ModifyHealth(-damage);
        LogManager.Log($"피해 받음. 대미지 : {damage}, 현재 체력 : {Status.GetCurrentHealth()}/{Status.GetMaxHealth()}", ELogType.Test, ELogCategory.GameSystem);
    }
}
