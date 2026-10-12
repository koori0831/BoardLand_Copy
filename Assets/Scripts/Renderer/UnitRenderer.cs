using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text deffendPoint;
    [SerializeField] private float hpAnimationDuration = 0.3f;

    private Unit unit;
    private MotionHandle hpMotion;

    public virtual void Bind(Unit unit)
    {
        this.unit = unit;

        Initialize(unit.Status.GetMaxHealth());
        SetHPUI(unit.Status.GetCurrentHealth(), false);
        ChangeDeffendPoint(unit.Status.GetCurrentDeffendPoint());

        unit.Status.HealthChanged += OnHealthChanged;
        unit.Status.DeffendPointChanged += ChangeDeffendPoint;
    }

    public void Initialize(int maxHp)
    {
        hpMotion.TryCancel();
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }

    public void SetHPUI(int hp, bool withAnimation = true)
    {
        hpMotion.TryCancel();

        if (withAnimation == false)
        {
            hpSlider.value = hp;
            return;
        }

        hpMotion = LMotion.Create(hpSlider.value, hp, hpAnimationDuration)
            .WithEase(Ease.OutQuad)
            .Bind(AnimateHPUI)
            .AddTo(this);
    }

    public void ChangeDeffendPoint(int point)
    {
        deffendPoint.text = point.ToString();
    }

    private void OnHealthChanged(int currentHp, int maxHp)
    {
        SetHPUI(currentHp);
    }

    private void AnimateHPUI(float value)
    {
        hpSlider.value = value;
    }

    protected virtual void OnDestroy()
    {
        if (unit == null)
            return;

        unit.Status.HealthChanged -= OnHealthChanged;
        unit.Status.DeffendPointChanged -= ChangeDeffendPoint;
    }
}
