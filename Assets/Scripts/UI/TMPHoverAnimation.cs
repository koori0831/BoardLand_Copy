using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class TMPHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text tmp;

    private int lastCharCount = -1;
    private bool isHovering;
    private MotionHandle currentMotion;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }

    private void PlayAnimation()
    {
        if (tmp == null || !isHovering)
            return;

        lastCharCount++;

        if (lastCharCount >= tmp.textInfo.characterCount)
            lastCharCount = 0;
        if (tmp.text[lastCharCount] == '\n' || tmp.text[lastCharCount] == ' ')
        {
            PlayAnimation();
            return;
        }

        currentMotion = LMotion.Create(Vector3.zero, Vector3.up * 5f, 0.1f)
            .WithLoops(2, LoopType.Yoyo)
            .WithOnComplete(PlayAnimation)
            .BindToTMPCharPosition(tmp, lastCharCount)
            .AddTo(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering)
            return;

        isHovering = true;
        if (currentMotion.IsActive() == false)
            PlayAnimation();
    }

    public void OnPointerExit(PointerEventData eventData) => isHovering = false;
}