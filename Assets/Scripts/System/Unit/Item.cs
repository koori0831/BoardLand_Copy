public class Item
{
    public ItemData Data { get; private set; }
    public Skill UseSkill => Data.UseSkill;

    public Item(ItemData data)
    {
        Data = data;
    }
}
