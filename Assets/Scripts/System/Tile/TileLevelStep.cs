public class TileLevelStep
{
    // 누적 필요 경험치와 Skill 정보.
    public int RequiredEXP;
    public Skill Skill;

    public TileLevelStep(int requiredEXP, Skill skill)
    {
        RequiredEXP = requiredEXP;
        Skill = skill;
    }
}
