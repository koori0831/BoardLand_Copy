using UnityEngine;

namespace Core
{
    // UI 표시를 담당한다. 사용자 입력은 각 View의 이벤트로 Presenter에 전달한다.
    public abstract class ViewBase : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        public bool UIEnable => uiEnable;
        private bool uiEnable;

        public virtual void HideUIRoot()
        {
            if (canvasGroup == null)
                return;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            uiEnable = false;
        }

        public virtual void ShowUIRoot()
        {
            if (canvasGroup == null)
                return;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            uiEnable = true;
        }
    }
}
