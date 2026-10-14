using Core;
using System.Collections.Generic;

public class Tiles
{
    public List<Tile> TileList;

    public Tiles(int boardSize, Dictionary<EElementType, TileLevelData> tileLevelDatas)
    {
        TileList = new List<Tile>();
        for (int i = 0; i < boardSize; i++)
        {
            ETileType tileType = new RandomEnumValue<ETileType>().RandomEnum;
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            TileList.Add(TileFactory.Create(tileType, elementType, tileLevelDatas[elementType]));
        }
    }
}