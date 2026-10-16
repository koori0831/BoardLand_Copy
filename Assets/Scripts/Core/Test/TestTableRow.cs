public class TestTableRow : TableRow
{
    public string Text;
    public EElementType ElementType;
    public ISkillCondition SkillCondiotion;
    public TestTableRow(string id, string text, EElementType elementType, ISkillCondition skillCondition) : base(id)
    {
        Text = text;
        ElementType = elementType;
        SkillCondiotion = skillCondition;
    }
}
