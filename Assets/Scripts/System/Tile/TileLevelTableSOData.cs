using System;

[Serializable]
public class TileLevelTableSOData
{
    public int RequiredEXP;
    public SkillSO Skill;

    public TileLevelStep ConvertTileLevelStep()
    {
        if (Skill == null)
            return new TileLevelStep(RequiredEXP, null);

        return new TileLevelStep(RequiredEXP, new Skill(Skill.SkillConditions, Skill.SkillEffects));
    }
}
