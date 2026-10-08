using Core;
using System.Collections.Generic;

public class Tiles
{
    public List<Tile> TileList;

    public Tiles(int boardSize)
    {
        TileList = new List<Tile>();
        for (int i = 0; i < boardSize; i++)
        {
            EElementType elementType = new RandomEnumValue<EElementType>().RandomEnum;
            ETileType tileType = new RandomEnumValue<ETileType>().RandomEnum;
            TileList.Add(new Tile(elementType, tileType));
        }
    }
}