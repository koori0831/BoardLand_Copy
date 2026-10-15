using UnityEngine;

public class ItemData
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;
    public int Price;
    public Skill UseSkill;

    public ItemData(string itemName, string itemDescription, Sprite itemIcon, int price, Skill useSkill)
    {
        ItemName = itemName;
        ItemDescription = itemDescription;
        ItemIcon = itemIcon;
        Price = price;
        UseSkill = useSkill;
    }
}
