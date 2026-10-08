public class TileActionContext
{
    public Player Player;
    public Enemy Enemy;
    public Dice Dice;
    public TileActionContext(Player player, Enemy enemy, Dice dice)
    {
        Player = player;
        Enemy = enemy;
        Dice = dice;
    }
}