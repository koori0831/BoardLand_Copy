using System.Collections.Generic;
using UnityEngine;

public class GameRenderer : MonoBehaviour
{
    [SerializeField] private PlayerRenderer playerRenderer;
    [SerializeField] private UnitRenderer enemyRenderer;
    [SerializeField] private Vector3 playerPositionOffset;

    private Game game;
    private BoardRenderer boardRenderer;
    private Vector3 playerSlotOffset;

    public void Bind(Game game, BoardRenderer boardRenderer, Vector3 playerSlotOffset)
    {
        this.game = game;
        this.boardRenderer = boardRenderer;
        this.playerSlotOffset = playerSlotOffset;

        playerRenderer.Bind(game.Player);
        enemyRenderer.Bind(game.Enemy);
        playerRenderer.Teleport(GetStandPosition(game.PlayerPosition));

        game.PlayerMoved += OnPlayerMoved;
    }

    public void SetFocused(bool isFocused)
    {
        enemyRenderer.gameObject.SetActive(isFocused);
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
        return boardRenderer.GetTilePosition(tileIndex, playerSlotOffset) + playerPositionOffset;
    }
}
