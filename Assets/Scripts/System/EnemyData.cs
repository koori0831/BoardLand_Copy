using System;
using System.Collections.Generic;

[Serializable]
public struct EnemyData
{
    public string EnemyName;
    public string EnemyDescription;
    // public List<SkillSO> Skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    public UnitStatus Status;
}