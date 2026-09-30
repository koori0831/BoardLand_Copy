using UnityEngine.InputSystem;

public class TitlePopupManager : MVPUIManagerBase<TitlePopupEvent, TitlePopupData>
{
    protected override void Awake()
    {
        model = new TitlePopupModel();
        presenter = new TitlePopupPresenter();
        base.Awake();
    }

    public void Update()
    {
        if (Keyboard.current.spaceKey.IsPressed())
        {
            EventBus<TitlePopupEvent>.Publish(new(new(true)));
        }
        if (Keyboard.current.enterKey.IsPressed())
        {
            EventBus<TitlePopupEvent>.Publish(new(new(false)));
        }
    }
}
