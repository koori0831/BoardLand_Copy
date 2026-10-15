using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPopupUI : MonoBehaviour
{
    [SerializeField] private ShopProductUI productTemplate;
    [SerializeField] private Transform productParent;
    [SerializeField] private TMP_Text goldText;

    private Board board;
    private List<(ShopProductUI productUI, Func<Game, bool> canBuy)> products = new();

    public void Bind(Board board, List<ItemData> itemDatas, List<WeaponData> weaponDatas)
    {
        this.board = board;
        foreach (Game game in board.Games)
            game.ShopOpened += OpenPopup;
        foreach (var product in products)
            Destroy(product.productUI.gameObject);
        products.Clear();

        foreach (ItemData itemData in itemDatas)
        {
            ShopProductUI productUI = CreateProductUI();
            productUI.SetProduct(itemData.ItemIcon, itemData.ItemName, itemData.ItemDescription, itemData.Price, () => Buy(game => game.Player.BuyItem(itemData)));
            products.Add((productUI, game => game.Player.IsItemInventoryFull == false && game.Player.Gold >= itemData.Price));
        }
        foreach (WeaponData weaponData in weaponDatas)
        {
            ShopProductUI productUI = CreateProductUI();
            productUI.SetProduct(weaponData.WeaponIcon, weaponData.WeaponName, weaponData.WeaponDescription, weaponData.Price, () => Buy(game => game.Player.BuyWeapon(weaponData)));
            products.Add((productUI, game => game.Player.Gold >= weaponData.Price));
        }
        gameObject.SetActive(false);
    }

    private ShopProductUI CreateProductUI()
    {
        ShopProductUI productUI = Instantiate(productTemplate, productParent);
        productUI.gameObject.SetActive(true);
        return productUI;
    }

    public void OpenPopup()
    {
        gameObject.SetActive(true);
        Refresh();
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    private void Buy(Func<Game, bool> buyAction)
    {
        buyAction.Invoke(board.Games[board.CurrentGameIndex]);
        Refresh();
    }

    private void Refresh()
    {
        Game game = board.Games[board.CurrentGameIndex];
        goldText.text = game.Player.Gold.ToString();
        foreach (var product in products)
            product.productUI.SetBuyable(product.canBuy(game));
    }
}
