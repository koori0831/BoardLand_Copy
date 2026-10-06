using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Scriptable Objects/Boss")]
public class BossDataSO : ScriptableObject
{
    public string bossName;
    public string bossDescription;
    public List<SkillSO> skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    public UnitStatus status;
}
