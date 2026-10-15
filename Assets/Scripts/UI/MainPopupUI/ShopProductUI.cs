using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopProductUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private Action buyCallback;

    public void SetProduct(Sprite iconSprite, string productName, string description, int price, Action buyCallback)
    {
        icon.sprite = iconSprite;
        nameText.text = productName;
        descriptionText.text = description;
        priceText.text = price.ToString();
        this.buyCallback = buyCallback;
    }

    public void SetBuyable(bool isBuyable)
    {
        buyButton.interactable = isBuyable;
    }

    public void ClickBuy()
    {
        buyCallback?.Invoke();
    }
}
