using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitRenderer : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private TMP_Text deffendPoint;

    public void Initialize(int maxHp)
    {
        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;
    }

    public void SetHPUI(int changeValue, bool withAnimation = true)
    {
        LMotion.Create(hpSlider.value, changeValue, 0.3f)
            .Bind(AnimateHPUI);
    }
    private void AnimateHPUI(float value)
    {
        hpSlider.value = (int)value;
    }
    public void ChangeDeffendPoint(int point)
    {
        deffendPoint.text = point.ToString();
    }
}
