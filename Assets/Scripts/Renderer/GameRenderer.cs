using System.Collections.Generic;
using UnityEngine;

public class GameRenderer : MonoBehaviour
{
    [SerializeField] private PlayerRenderer playerRenderer;
    [SerializeField] private UnitRenderer enemyRenderer;

    private Game game;
    private BoardRenderer boardRenderer;

    public void Bind(Game game, BoardRenderer boardRenderer)
    {
        this.game = game;
        this.boardRenderer = boardRenderer;

        playerRenderer.Bind(game.Player);
        enemyRenderer.Bind(game.Enemy);
        playerRenderer.Teleport(GetStandPosition(game.PlayerPosition));

        game.PlayerMoved += OnPlayerMoved;
    }

    private void OnDestroy()
    {
        if (game != null)
            game.PlayerMoved -= OnPlayerMoved;
    }

    private void OnPlayerMoved(int fromTileIndex, int steps)
    {
        List<Vector3> path = new List<Vector3>(steps);
        for (int i = 1; i <= steps; i++)
            path.Add(GetStandPosition(fromTileIndex + i));

        playerRenderer.Move(path);
    }

    private Vector3 GetStandPosition(int tileIndex)
    {
        return boardRenderer.GetTilePosition(tileIndex);
    }
}
