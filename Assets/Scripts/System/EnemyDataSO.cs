using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;
    public List<SkillSO> Skills; // 보스는 스킬 목록에서 사용 가능한 스킬을 랜덤으로 선택해 사용한다.
    public UnitStatus Status;

    public EnemyData ConvertEnemyData()
    {
        List<Skill> skillList = new();
        foreach (SkillSO skillSO in Skills)
            skillList.Add(new Skill(skillSO.skillName, skillSO.skillDescription, skillSO.skillIcon, skillSO.skillConditions, skillSO.skillEffects));

        return new EnemyData(EnemyName, EnemyDescription, skillList, Status);
    }
}
