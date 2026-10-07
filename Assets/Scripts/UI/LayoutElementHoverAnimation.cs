using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using LitMotion;
using System.Collections.Generic;

public enum ELayoutElementVariableType
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
    [SerializeField] private ELayoutElementVariableType variableType;
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
        return variableType switch
        {
            ELayoutElementVariableType.MinWidth => layoutElement.minWidth,
            ELayoutElementVariableType.MinHeight => layoutElement.minHeight,
            ELayoutElementVariableType.PreferredWidth => layoutElement.preferredWidth,
            ELayoutElementVariableType.PreferredHeight => layoutElement.preferredHeight,
            ELayoutElementVariableType.FlexibleWidth => layoutElement.flexibleWidth,
            ELayoutElementVariableType.FlexibleHeight => layoutElement.flexibleHeight,
            ELayoutElementVariableType.MaxWidth => layoutElement.maxWidth,
            ELayoutElementVariableType.MaxHeight => layoutElement.maxHeight,
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
            case ELayoutElementVariableType.MaxWidth:
                layoutElement.maxWidth = changedValue; break;
            case ELayoutElementVariableType.MaxHeight:
                layoutElement.maxHeight = changedValue; break;
        }
    }
}
