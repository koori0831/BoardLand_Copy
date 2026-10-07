public struct DiceCard
{
    public EElementType ElementType;
    public int DiceNumder;

    public DiceCard(int diceNumber, EElementType elementType = EElementType.None)
    {
        DiceNumder = diceNumber;
        ElementType = elementType;
    }
}
