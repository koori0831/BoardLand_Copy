using Core;
using System;
using System.Collections.Generic;

public class Tiles
{
    public List<Tile> TileList;
    public event Action<int> TileChanged;
    private Dictionary<EElementType, TileLevelData> tileLevelDatas;

    public Tiles(int boardSize, Dictionary<EElementType, TileLevelData> tileLevelDatas)
    {
        this.tileLevelDatas = tileLevelDatas;
        TileList = new List<Tile>();
        for (int i = 0; i < boardSize; i++)
        {
            ETileType tileType = new RandomEnumValue<ETileType>().RandomEnum;
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            TileList.Add(TileFactory.Create(tileType, elementType, tileLevelDatas[elementType]));
        }
    }

    public void ReplaceTile(int tileIndex, ETileType tileType, EElementType elementType)
    {
        TileList[tileIndex] = TileFactory.Create(tileType, elementType, tileLevelDatas[elementType]);
        TileChanged?.Invoke(tileIndex);
    }

    public void ReplaceToRandomTile(int tileIndex, ETileType exceptTileType)
    {
        ETileType nextTileType = new RandomEnumValue<ETileType>().RandomEnum;
        while (nextTileType == exceptTileType)
            nextTileType = new RandomEnumValue<ETileType>().RandomEnum;
        EElementType nextElementType = new RandomEnumValue<EElementType>().RandomEnum;
        ReplaceTile(tileIndex, nextTileType, nextElementType);
    }
}