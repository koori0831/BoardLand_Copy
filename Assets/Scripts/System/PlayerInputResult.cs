public class PlayerInputResult
{
    public EPlayerInputType ActionType;
    public Dice Dice;
    public PlayerInputResult(EPlayerInputType actionType, Dice dice)
    {
        ActionType = actionType;
        Dice = dice;
    }
}