using UnityEngine;

public class PlayTypeSelectPopupUI : MonoBehaviour
{
    [SerializeField] private PlayerCountSelectPopupUI playerCountSelectPopupUI;

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    public void ClickLocalPlay()
    {
        ClosePopup();
        playerCountSelectPopupUI.OpenPopup();
    }
}
