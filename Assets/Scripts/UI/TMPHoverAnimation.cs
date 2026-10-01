using LitMotion;
using LitMotion.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI tmp;
    private int lastCharCount = 0;
    private bool isHovering = false;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void PlayAnimation()
    {
        if (tmp == null || isHovering == false)
            return;

        if (lastCharCount >= tmp.textInfo.characterCount)
            lastCharCount = 0;

        LSequence.Create()
            .Append(LMotion.Create(Vector3.zero, Vector3.up * 10f, 0.15f).BindToTMPCharPosition(tmp, lastCharCount))
            .Append(LMotion.Create(Vector3.up * 10f, Vector3.zero, 0.15f).WithOnComplete(AfterAnimation).BindToTMPCharPosition(tmp, lastCharCount))
            .Run();
    }

    private void AfterAnimation()
    {
        lastCharCount++;
        PlayAnimation();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        PlayAnimation();
    }
    public void OnPointerExit(PointerEventData eventData) => isHovering = false;
}
