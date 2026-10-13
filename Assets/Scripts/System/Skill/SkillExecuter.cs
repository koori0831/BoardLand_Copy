using System.Collections.Generic;

public static class SkillExecuter
{
    // 어디까지 실행했는지 기록
    private static int currentSkillEffectIndex = 0;
    // 스킬 효과 목록 스냅샷 캐싱
    private static List<SkillEffectBase> effects;

    public static void ExecuteSkill(Skill skill, SkillContext context)
    {
        // 우선 이 스킬이 실행 가능한지 확인
        if (skill.CanUse(context) == false)
            return;

        // 가능하다면 상태를 한번 정리한다
        effects.Clear();
        currentSkillEffectIndex = 0;

        // 스킬 효과 목록을 캐싱하고 실행
        effects = skill.skillEffects;
        ExecuteSkillEffect(context);
        return;
    }

    private static void ExecuteSkillEffect(SkillContext context)
    {
        // 만약 아직 실행되지 않은 스킬 효과가 있다면 실행
        if (currentSkillEffectIndex < effects.Count)
        {
            // 스킬 실행이 끝나면 다음 스킬 효과를 실행하도록 콜백을 전달
            effects[currentSkillEffectIndex].ApplyEffect(context, () => ExecuteSkillEffect(context));
            currentSkillEffectIndex++;
        }
        else
        {
            // 모든 스킬 효과가 실행되었으므로 상태를 정리
            effects.Clear();
            currentSkillEffectIndex = 0;
        }
    }
}