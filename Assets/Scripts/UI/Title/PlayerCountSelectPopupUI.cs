using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCountSelectPopupUI : MonoBehaviour
{
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    public void ClickPlayerCount(int playerCount)
    {
        LocalGameSetting.PlayerCount = Mathf.Clamp(playerCount, GameConstant.MinPlayerCount, GameConstant.MaxPlayerCount);
        SceneManager.LoadScene("Game");
    }
}
