using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPopupUI : MonoBehaviour
{
    private static readonly string volumeKey = "MasterVolume";

    [SerializeField] private Slider volumeSlider;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadSetting()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(volumeKey, 1f);
    }

    public void OpenPopup()
    {
        volumeSlider.SetValueWithoutNotify(AudioListener.volume);
        gameObject.SetActive(true);
    }

    public void ClosePopup()
    {
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }

    public void OnVolumeChanged(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumeKey, volume);
    }

    public void ClickGoToTitle()
    {
        SceneManager.LoadScene("Ttitle");
    }
}
