using System.Collections.Generic;
using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
    [SerializeField] private GameRenderer gameRendererA;
    [SerializeField] private GameRenderer gameRendererB;
    [SerializeField] private List<TileRenderer> tileRenderers;

    public void TileInitialize(Tiles tiles)
    {
        foreach (Tile tile in tiles.TileList)
        {

        }
    }
}
