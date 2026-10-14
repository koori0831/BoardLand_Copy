using System.Collections.Generic;
using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
    [SerializeField] private TileRenderer tileRendererPrefab;
    [SerializeField] private GameRenderer gameRendererPrefab;

    [SerializeField] private float tileRadius = 4f;
    [SerializeField] private Vector3 tilePositionOffest;
    [SerializeField] private float playerSlotSpacing = 0.8f;

    private List<TileRenderer> tileRenderers = new();
    private List<GameRenderer> gameRenderers = new();
    private Board board;

    public void Bind(Board board)
    {
        Clear();
        this.board = board;

        CreateTileRenderers(board.Tiles);
        CreateGameRenderers(board.Games);

        OnCurrentGameChanged(board.CurrentGameIndex);
        board.CurrentGameChanged += OnCurrentGameChanged;
        board.Tiles.TileChanged += OnTileChanged;
    }

    public Vector3 GetTilePosition(int tileIndex)
    {
        return tileRenderers[tileIndex % tileRenderers.Count].transform.position;
    }

    public Vector3 GetTilePosition(int tileIndex, Vector3 localOffset)
    {
        Transform tileTransform = tileRenderers[tileIndex % tileRenderers.Count].transform;
        return tileTransform.position + tileTransform.rotation * localOffset;
    }

    private void OnCurrentGameChanged(int currentIndex)
    {
        for (int i = 0; i < gameRenderers.Count; i++)
            gameRenderers[i].SetFocused(i == currentIndex);
    }

    private void OnTileChanged(int changedTileIndex)
    {
        tileRenderers[changedTileIndex].Bind(board.Tiles.TileList[changedTileIndex]);
    }

    private void CreateTileRenderers(Tiles tiles)
    {
        int tileCount = tiles.TileList.Count;
        for (int i = 0; i < tileCount; i++)
        {
            TileRenderer tileRenderer = Instantiate(tileRendererPrefab, transform);
            tileRenderer.transform.localPosition = GetTileLocalPosition(i, tileCount);
            tileRenderer.transform.LookAt(transform);
            tileRenderer.transform.localPosition += tilePositionOffest;
            tileRenderer.Bind(tiles.TileList[i]);
            tileRenderers.Add(tileRenderer);
        }
    }

    private void CreateGameRenderers(List<Game> games)
    {
        for (int i = 0; i < games.Count; i++)
        {
            GameRenderer gameRenderer = Instantiate(gameRendererPrefab, transform);
            Vector3 slotOffset = Vector3.right * ((i - (games.Count - 1) * 0.5f) * playerSlotSpacing);
            gameRenderer.Bind(games[i], this, slotOffset);
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
        if (board != null)
        {
            board.CurrentGameChanged -= OnCurrentGameChanged;
            board.Tiles.TileChanged -= OnTileChanged;
        }
        board = null;

        foreach (TileRenderer tileRenderer in tileRenderers)
            Destroy(tileRenderer.gameObject);
        foreach (GameRenderer gameRenderer in gameRenderers)
            Destroy(gameRenderer.gameObject);

        tileRenderers.Clear();
        gameRenderers.Clear();
    }

    private void OnDestroy()
    {
        if (board != null)
        {
            board.CurrentGameChanged -= OnCurrentGameChanged;
            board.Tiles.TileChanged -= OnTileChanged;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, tileRadius);
    }
}
