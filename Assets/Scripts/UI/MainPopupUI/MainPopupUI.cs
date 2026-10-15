using Core;
using TMPro;
using UnityEngine;

public class MainPopupUI : MonoBehaviour
{
    [SerializeField] private TMP_Text turnCountText;
    [SerializeField] private TMP_Text lapCountText;
    [SerializeField] private TMP_Text goldText;
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
        UnbindGame();

        game = board.Games[currentGameIndex];
        OnTurnCountChanged(game.TurnCount);
        OnLapCountChanged(game.LapCount);
        OnGoldChanged(game.Player.Gold);

        game.TurnCountChanged += OnTurnCountChanged;
        game.LapCountChanged += OnLapCountChanged;
        game.Player.GoldChanged += OnGoldChanged;
    }

    private void UnbindGame()
    {
        if (game == null)
            return;
        game.TurnCountChanged -= OnTurnCountChanged;
        game.LapCountChanged -= OnLapCountChanged;
        game.Player.GoldChanged -= OnGoldChanged;
    }

    private void OnTurnCountChanged(int turnCount)
    {
        turnCountText.text = turnCount.ToString("00");
    }

    private void OnLapCountChanged(int lapCount)
    {
        lapCountText.text = lapCount.ToString("00");
    }

    private void OnGoldChanged(int gold)
    {
        goldText.text = gold.ToString();
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
        UnbindGame();
    }
}
