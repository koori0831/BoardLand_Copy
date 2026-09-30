using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/CardData")]
public class TileDataSO : ScriptableObject
{
    [SerializeReference] private List<ITileRule> moveRules;

    public bool CheckCanMoveThisTile()
    {
        foreach (ITileRule rule in moveRules)
        {
            if (rule.CheckValidConditions() == false)
                return false;
        }
        return true;
    }
}
