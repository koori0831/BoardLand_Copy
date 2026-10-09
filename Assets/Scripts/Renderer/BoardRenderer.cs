using System.Collections.Generic;
using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
    [SerializeField] private TileRenderer tileRendererPrefab;
    [SerializeField] private GameRenderer gameRendererPrefab;

    [SerializeField] private float tileRadius = 4f;

    private List<TileRenderer> tileRenderers = new();
    private List<GameRenderer> gameRenderers = new();

    public void Bind(Board board)
    {
        Clear();

        CreateTileRenderers(board.Tiles);
        CreateGameRenderers(board.Games);
    }

    public Vector3 GetTilePosition(int tileIndex)
    {
        return tileRenderers[tileIndex % tileRenderers.Count].transform.position;
    }

    private void CreateTileRenderers(Tiles tiles)
    {
        int tileCount = tiles.TileList.Count;
        for (int i = 0; i < tileCount; i++)
        {
            TileRenderer tileRenderer = Instantiate(tileRendererPrefab, transform);
            tileRenderer.transform.localPosition = GetTileLocalPosition(i, tileCount);
            tileRenderer.transform.LookAt(transform);
            tileRenderer.Bind(tiles.TileList[i]);
            tileRenderers.Add(tileRenderer);
        }
    }

    private void CreateGameRenderers(List<Game> games)
    {
        for (int i = 0; i < games.Count; i++)
        {
            GameRenderer gameRenderer = Instantiate(gameRendererPrefab, transform);
            gameRenderer.Bind(games[i], this);
            gameRenderers.Add(gameRenderer);
        }
    }

    private Vector3 GetTileLocalPosition(int index, int tileCount)
    {
        float angle = 2f * Mathf.PI * index / tileCount;
        return new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * tileRadius;
    }

    private void Clear()
    {
        foreach (TileRenderer tileRenderer in tileRenderers)
            Destroy(tileRenderer.gameObject);
        foreach (GameRenderer gameRenderer in gameRenderers)
            Destroy(gameRenderer.gameObject);

        tileRenderers.Clear();
        gameRenderers.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, tileRadius);
    }
}
