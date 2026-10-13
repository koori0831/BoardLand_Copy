using System;
using System.Collections.Generic;

public class EnemyData
{
    public string EnemyName;
    public string EnemyDescription;
    public List<Skill> Skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    public UnitStatus Status;

    public EnemyData(string enemyName, string enemyDescription, List<Skill> skills, UnitStatus status)
    {
        EnemyName = enemyName;
        EnemyDescription = enemyDescription;
        Skills = skills;
        Status = status;
    }
}