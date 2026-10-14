using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndPopupUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text rankText;

    private Board board;

    public void Bind(Board board)
    {
        if (this.board != null)
            this.board.AllGameEnd -= OnAllGameEnd;

        this.board = board;
        board.AllGameEnd += OnAllGameEnd;
        gameObject.SetActive(false);
    }

    private void OnAllGameEnd()
    {
        // 리더보드의 첫번째가 1등
        resultText.text = $"Player{board.ReaderBoard[0] + 1} Win!";

        StringBuilder rankBuilder = new StringBuilder();
        for (int i = 0; i < board.ReaderBoard.Count; i++)
            rankBuilder.AppendLine($"{i + 1}등  Player{board.ReaderBoard[i] + 1}");
        rankText.text = rankBuilder.ToString();

        gameObject.SetActive(true);
    }

    public void ClickGoToTitle()
    {
        SceneManager.LoadScene("Ttitle");
    }

    private void OnDestroy()
    {
        if (board != null)
            board.AllGameEnd -= OnAllGameEnd;
    }
}
