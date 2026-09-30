using UnityEngine;

public class TitlePopupView : MonoBehaviour
{
    public ObservableProperty<string> testString = new("test");

    private void Start()
    {
        Debug.Log(testString.Value);
    }
}
