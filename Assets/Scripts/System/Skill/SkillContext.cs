public enum ESkillTarget
{
    Self,
    Enemy,
}

public class SkillContext
{
    // public Game Game;
    public Unit SelfUnit;
    public Unit EnemyUnit;
    public Tiles Tiles;
    public int LastSkillIndex = -1;
    public SkillContext(Unit selfUnit, Unit enemyUnit, Tiles tiles)
    {
        // Game = game;
        SelfUnit = selfUnit;
        EnemyUnit = enemyUnit;
        Tiles = tiles;
    }

    public Unit GetTargetUnit(ESkillTarget target)
    {
        return target switch
        {
            ESkillTarget.Self => SelfUnit,
            ESkillTarget.Enemy => EnemyUnit,
            _ => SelfUnit
        };
    }
}