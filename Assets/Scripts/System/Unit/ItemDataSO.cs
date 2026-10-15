using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemDataSO : ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite ItemIcon;
    public int Price;

    public SkillSO UseSkill;

    public ItemData ConvertItemData()
    {
        Skill useSkill = null;
        if (UseSkill != null)
            useSkill = new Skill(UseSkill.SkillConditions, UseSkill.SkillEffects);

        return new ItemData(ItemName, ItemDescription, ItemIcon, Price, useSkill);
    }
}
