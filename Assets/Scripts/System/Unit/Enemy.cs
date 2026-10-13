using System.Collections.Generic;

public class Enemy : Unit
{
    public List<Skill> Skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    private List<Skill> usableSkills;

    public Enemy(UnitStatus status) : base(status)
    {
        usableSkills = new List<Skill>();
    }

    public Skill GetNextSkill(SkillContext context)
    {
        usableSkills.Clear();
        foreach (Skill skill in Skills)
        {
            if (skill.CanUse(context))
                usableSkills.Add(skill);
        }
        context.LastSkillIndex = usableSkills.Count - 1;
        return usableSkills[^1]; // 리스트의 뒤에 등록된 스킬일 수록 우선도가 높음.
    }
}
