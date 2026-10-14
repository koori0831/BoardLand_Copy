using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileLevelTable", menuName = "Scriptable Objects/TileLevelTable")]
public class TileLevelTableSO : ScriptableObject
{
    public List<TileLevelTableSOData> TileLevelDataList;

    public TileLevelData ConvertTileLevelData()
    {
        TileLevelData tileLevelData = new TileLevelData();
        foreach (TileLevelTableSOData soData in TileLevelDataList)
            tileLevelData.LevelSteps.Add(soData.ConvertTileLevelStep());
        return tileLevelData;
    }
}
