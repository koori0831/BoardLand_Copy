using Core;
using TMPro;
using UnityEngine;

public class MainPopupUI : MonoBehaviour
{
    [SerializeField] private TMP_Text turnCountText;
    [SerializeField] private SettingPopupUI settingPopupUI;

    private Board board;
    private Game game;

    public void Bind(Board board)
    {
        if (this.board != null)
            this.board.CurrentGameChanged -= OnGameChanged;

        this.board = board;
        board.CurrentGameChanged += OnGameChanged;
    }

    private void OnGameChanged(int currentGameIndex)
    {
        if (game != null)
            game.TurnCountChanged -= OnTurnCountChanged;

        game = board.Games[currentGameIndex];
        OnTurnCountChanged(game.TurnCount);

        game.TurnCountChanged += OnTurnCountChanged;
    }

    private void OnTurnCountChanged(int turnCount)
    {
        turnCountText.text = turnCount.ToString("00");
    }

    public void ClickTurnEnd()
    {
        EventBus<PlayerInputEvent>.Publish(new(EPlayerInputType.TurnEnd, 0));
    }

    public void ClickSetting()
    {
        settingPopupUI.OpenPopup();
    }

    private void OnDestroy()
    {
        if (board != null)
            board.CurrentGameChanged -= OnGameChanged;
        if (game != null)
            game.TurnCountChanged -= OnTurnCountChanged;
    }
}
