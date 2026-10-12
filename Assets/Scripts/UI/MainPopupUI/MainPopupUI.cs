using Core;
using UnityEngine;

public class MainPopupUI : MonoBehaviour
{
    public void ClickTurnEnd()
    {
        EventBus<PlayerInputEvent>.Publish(new(EPlayerInputType.TurnEnd, 0));
    }
}
