using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;

public enum ELayoutElementVariableType
{
    MinWidth,
    MinHeight,
    PreferredWidth,
    PreferredHeight,
    FlexibleWidth,
    FlexibleHeight,
}

[RequireComponent(typeof(LayoutElement))]
public class LayoutElementHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ELayoutElementVariableType variableType;
    [SerializeField] private float hoverValue = -1f;
    [SerializeField] private float animationTime = 0.3f;
    [SerializeField] private Ease easeType = Ease.Linear;
    private float originalValue = -1f;
    private LayoutElement layoutElement;
    private MotionHandle currentMotionHandle = new();

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        originalValue = GetLayoutElementVariable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentMotionHandle.TryCancel();
        currentMotionHandle = (LMotion.Create(GetLayoutElementVariable(), hoverValue, animationTime)
            .WithEase(easeType)
            .Bind(_ => SetLayoutElementVariable(_))
            .AddTo(this));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentMotionHandle.TryCancel();
        currentMotionHandle = (LMotion.Create(GetLayoutElementVariable(), originalValue, animationTime)
            .WithEase(easeType)
            .Bind(_ => SetLayoutElementVariable(_))
            .AddTo(this));
    }

    private float GetLayoutElementVariable()
    {
        return variableType switch
        {
            ELayoutElementVariableType.MinWidth => layoutElement.minWidth,
            ELayoutElementVariableType.MinHeight => layoutElement.minHeight,
            ELayoutElementVariableType.PreferredWidth => layoutElement.preferredWidth,
            ELayoutElementVariableType.PreferredHeight => layoutElement.preferredHeight,
            ELayoutElementVariableType.FlexibleWidth => layoutElement.flexibleWidth,
            ELayoutElementVariableType.FlexibleHeight => layoutElement.flexibleHeight,
            _ => -1.0f,
        };
    }
    private void SetLayoutElementVariable(float changedValue)
    {
        switch (variableType)
        {
            case ELayoutElementVariableType.MinWidth:
                layoutElement.minWidth = changedValue; break;
            case ELayoutElementVariableType.MinHeight:
                layoutElement.minHeight = changedValue; break;
            case ELayoutElementVariableType.PreferredWidth:
                layoutElement.preferredWidth = changedValue; break;
            case ELayoutElementVariableType.PreferredHeight:
                layoutElement.preferredHeight = changedValue; break;
            case ELayoutElementVariableType.FlexibleWidth:
                layoutElement.flexibleWidth = changedValue; break;
            case ELayoutElementVariableType.FlexibleHeight:
                layoutElement.flexibleHeight = changedValue; break;
        }
    }
}
