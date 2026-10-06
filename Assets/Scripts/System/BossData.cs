using System;
using System.Collections.Generic;
using System.Text;
using Unit;

public struct BossData
{
    public string bossName;
    public string bossDescription;
    //public EUnitType;
    public List<SkillData> skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    public UnitStatus status;
}
