using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public class SpriteList
{
    public List<Sprite> sprites = new();
}

public class DiceUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private DicePreviewUI previewUIPrefab;
    [SerializeField] private SerializableDictionary<EElementType, SpriteList> diceImages;
    private Action dropCallback;
    private DicePreviewUI previewUI;

    public void SetDiceUI(EElementType elementType, int diceNumber, Action dropCallback)
    {
        Sprite targetSprite = null;
        if (diceImages.TryGetValue(elementType, out SpriteList images))
            targetSprite = images?.sprites[diceNumber - 1];
        if (targetSprite == null)
            return;
        image.sprite = targetSprite;
        this.dropCallback = dropCallback;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(previewUI.gameObject);
        image.enabled = true;
        dropCallback.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previewUI = Instantiate(previewUIPrefab, transform.root);
        previewUI.SetImage(image.sprite);
        image.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
