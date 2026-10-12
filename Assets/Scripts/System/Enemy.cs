using System.Collections.Generic;

public class Enemy : Unit
{
    public List<Skill> Skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.

    public Enemy(UnitStatus status) : base(status)
    {
    }
}
