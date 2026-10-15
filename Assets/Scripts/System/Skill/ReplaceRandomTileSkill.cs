using Core;
using System;
using System.Collections.Generic;

// 보드의 랜덤한 타일 여러 개를 랜덤한 타일로 바꾼다.
[Serializable]
public class ReplaceRandomTileSkill : SkillEffectBase
{
    public int ReplaceCount = 3;

    public override void ApplyEffect(SkillContext context, Action endCallback)
    {
        Random random = new Random();
        List<int> tileIndexList = new List<int>();
        for (int i = 0; i < context.Tiles.TileList.Count; i++)
            tileIndexList.Add(i);

        for (int i = 0; i < ReplaceCount && tileIndexList.Count > 0; i++)
        {
            // 같은 타일이 두 번 바뀌지 않도록 뽑은 인덱스는 목록에서 뺀다.
            int pick = random.Next(0, tileIndexList.Count);
            int tileIndex = tileIndexList[pick];
            tileIndexList.RemoveAt(pick);

            ETileType tileType = new RandomEnumValue<ETileType>().RandomEnum;
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            context.Tiles.ReplaceTile(tileIndex, tileType, elementType);
            LogManager.Log($"{tileIndex}번 타일이 {elementType} {tileType} 타일로 바뀜.", ELogType.Test, ELogCategory.GameSystem);
        }
        base.ApplyEffect(context, endCallback);
    }
}
