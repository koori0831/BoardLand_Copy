using UnityEngine;

// 프레젠터와 모델에 대한 직접 참조 없이 EventBus를 통해 변화한 값을 모델에 전달한다.
public abstract class ViewBase<T, V> : MonoBehaviour where T : IUIEvent<V>
{
    [SerializeField] private CanvasGroup canvasGroup;
    public bool UIEnable 
    { 
        get => canvasGroup.interactable && canvasGroup.alpha == 1;
        set
        {
            if (value) ShowUIRoot();
            else HideUIRoot();
        }
    }
    // Bus로 이벤트를 쏜다.
    public abstract void UpdateModel(V UIData);

    public virtual void HideUIRoot()
    {
        if (canvasGroup == null)
            return;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    public virtual void ShowUIRoot()
    {
        if (canvasGroup == null)
            return;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}