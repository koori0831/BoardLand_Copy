using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileInfoUI : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<EElementType, Color> elementColor;
    [SerializeField] private SerializableDictionary<ETileType, Sprite> tileTypeIcon;
    [SerializeField] private Image elementTypeImage;
    [SerializeField] private Image sliderFillImage;
    [SerializeField] private Image icon;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text levelText;

    private MotionHandle currentMotion;

    private Board board;
    private Game game;
    private TileStatus tileStatus;

    public void Bind(Board board)
    {
        if (this.board != null)
            this.board.CurrentGameChanged -= OnGameChanged;

        this.board = board;
        game = board.Games[board.CurrentGameIndex];
        board.CurrentGameChanged += OnGameChanged;
        OnGameChanged(board.CurrentGameIndex);
    }

    private void OnGameChanged(int currnetGameIndex)
    {
        if (game != null)
            game.PlayerMoved -= OnPlayerMove;

        game = board.Games[currnetGameIndex];
        OnPlayerMove();

        game.PlayerMoved += OnPlayerMove;
    }

    private void OnPlayerMove(int from = 0, int diceNumber = 0)
    {
        if (tileStatus != null)
        {
            tileStatus.EXPChanged -= OnExpChange;
            tileStatus.LevelChanged -= OnLevelChange;
        }

        tileStatus = board.Tiles.TileList[game.PlayerPosition].Status;
        tileStatus.EXPChanged += OnExpChange;
        tileStatus.LevelChanged += OnLevelChange;

        OnLevelChange(tileStatus.CurrentLevel, tileStatus.IsMaxLevel);
        OnExpChange(tileStatus.Percentage);
        icon.sprite = tileTypeIcon[board.Tiles.TileList[game.PlayerPosition].TileType];

        elementTypeImage.color = elementColor[tileStatus.ElementType];
        sliderFillImage.color = elementColor[tileStatus.ElementType];
    }

    private void OnLevelChange(int level, bool isMaxLevel)
    {
        levelText.text = "Lv." + (isMaxLevel ? "MAX" : level);
    }

    private void OnExpChange(float percentage)
    {
        currentMotion.TryCancel();
        if (Mathf.Approximately(expSlider.value, percentage))
            return;
        if (percentage <= expSlider.value)
        {
            currentMotion = LSequence.Create()
                .Append(LMotion.Create(expSlider.value, expSlider.maxValue, 0.3f).Bind(_ => expSlider.value = _))
                .Append(currentMotion = LMotion.Create(expSlider.minValue, percentage, 0.3f).Bind(_ => expSlider.value = _))
                .Run()
                .AddTo(this);
        }
        else
        {
            currentMotion = LMotion.Create(expSlider.value, percentage, 0.3f)
                .Bind(_ => expSlider.value = _)
                .AddTo(this);
        }
    }

    private void OnDestroy()
    {
        if (board != null)
            board.CurrentGameChanged -= OnGameChanged;
        if (game != null)
            game.PlayerMoved -= OnPlayerMove;
        if (tileStatus != null)
        {
            tileStatus.EXPChanged -= OnExpChange;
            tileStatus.LevelChanged -= OnLevelChange;
        }
    }
}
