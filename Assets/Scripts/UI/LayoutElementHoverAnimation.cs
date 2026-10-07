using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;
using System.Collections.Generic;

public enum ELayoutElemetVariableType
{
    MinWidth,
    MinHeight,
    PreferredWidth,
    PreferredHeight,
    FlexibleWidth,
    FlexibleHeight,
    MaxWidth,
    MaxHeight,
}

[RequireComponent(typeof(LayoutElement))]
public class LayoutElementHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ELayoutElemetVariableType variableType;
    [SerializeField] private float hoverValue = -1f;
    [SerializeField] private float animationTime = 0.3f;
    [SerializeField] private Ease easeType = Ease.Linear;
    private float originalValue = -1f;
    private LayoutElement layoutElement;
    private List<MotionHandle> currentMotionHandles = new();

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        originalValue = GetLayoutElementVariable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentMotionHandles.Count > 0)
        {
            foreach (MotionHandle handle in currentMotionHandles)
                handle.TryCancel();
            AfterMotion();
        }
        currentMotionHandles.Add(LMotion.Create(GetLayoutElementVariable(), hoverValue, animationTime)
            .WithEase(easeType)
            .WithOnComplete(AfterMotion)
            .Bind(_ => SetLayoutElementVariable(_))
            .AddTo(this));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentMotionHandles.Count > 0)
        {
            foreach (MotionHandle handle in currentMotionHandles)
                handle.TryCancel();
            AfterMotion();
        }

        currentMotionHandles.Add(LMotion.Create(GetLayoutElementVariable(), originalValue, animationTime)
            .WithEase(easeType)
            .WithOnComplete(AfterMotion)
            .Bind(_ => SetLayoutElementVariable(_))
            .AddTo(this));
    }

    private void AfterMotion() => currentMotionHandles.Clear();

    private float GetLayoutElementVariable()
    {
        switch (variableType)
        {
            case ELayoutElemetVariableType.MinWidth:
                return layoutElement.minWidth;
            case ELayoutElemetVariableType.MinHeight:
                return layoutElement.minHeight;
            case ELayoutElemetVariableType.PreferredWidth:
                return layoutElement.preferredWidth;
            case ELayoutElemetVariableType.PreferredHeight:
                return layoutElement.preferredHeight;
            case ELayoutElemetVariableType.FlexibleWidth:
                return layoutElement.flexibleWidth;
            case ELayoutElemetVariableType.FlexibleHeight:
                return layoutElement.flexibleHeight;
            case ELayoutElemetVariableType.MaxWidth:
                return layoutElement.maxWidth;
            case ELayoutElemetVariableType.MaxHeight:
                return layoutElement.maxHeight;
            default: return -1.0f;
        }
    }
    private void SetLayoutElementVariable(float changedValue)
    {
        switch (variableType)
        {
            case ELayoutElemetVariableType.MinWidth:
                layoutElement.minWidth = changedValue; break;
            case ELayoutElemetVariableType.MinHeight:
                layoutElement.minHeight = changedValue; break;
            case ELayoutElemetVariableType.PreferredWidth:
                layoutElement.preferredWidth = changedValue; break;
            case ELayoutElemetVariableType.PreferredHeight:
                layoutElement.preferredHeight = changedValue; break;
            case ELayoutElemetVariableType.FlexibleWidth:
                layoutElement.flexibleWidth = changedValue; break;
            case ELayoutElemetVariableType.FlexibleHeight:
                layoutElement.flexibleHeight = changedValue; break;
            case ELayoutElemetVariableType.MaxWidth:
                layoutElement.maxWidth = changedValue; break;
            case ELayoutElemetVariableType.MaxHeight:
                layoutElement.maxHeight = changedValue; break;
        }
    }
}
