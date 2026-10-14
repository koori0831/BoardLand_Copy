using Core;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceDeckUI : MonoBehaviour
{
    [SerializeField] private DiceUI diceUIPrefab;
    [SerializeField] private DiceRollPopupUI diceRollPopupUI;
    [SerializeField] private List<RectTransform> actionArea;
    [SerializeField] private List<RectTransform> cancelArea;
    [SerializeField] private Camera uiCamera;
    private List<DiceUI> diceUIList = new();
    private List<Game> games = new();

    private void DiceDrop(int diceIndex)
    {
        EPlayerInputType playerInput = AreaCheck();
        if (playerInput == EPlayerInputType.Cancel)
            return;

        diceUIList[diceIndex].gameObject.SetActive(false);
        EventBus<PlayerInputEvent>.Publish(new PlayerInputEvent(playerInput, diceIndex));
    }

    private EPlayerInputType AreaCheck()
    {
        foreach (RectTransform area in actionArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Mouse.current.position.ReadValue(), uiCamera))
                return EPlayerInputType.Action;
        }
        foreach (RectTransform area in cancelArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Mouse.current.position.ReadValue(), uiCamera))
                return EPlayerInputType.Cancel;
        }
        return EPlayerInputType.Move;
    }

    public void Bind(List<Game> games)
    {
        Clear();
        this.games = games;
        foreach (Game game in games)
        {
            game.DiceRolled += DiceUISetting;
        }
    }

    private void DiceUISetting(List<Dice> dices)
    {
        foreach (DiceUI diceUI in diceUIList)
            Destroy(diceUI.gameObject);
        diceUIList.Clear();

        SpawnDiceUIAfterAnimation(dices).Forget();
    }

    private async UniTaskVoid SpawnDiceUIAfterAnimation(List<Dice> dices)
    {
        diceRollPopupUI.gameObject.SetActive(true);
        await diceRollPopupUI.PlayDiceRollAnimations(dices);
        await UniTask.WaitUntil(() => diceRollPopupUI.gameObject.activeSelf == false);
        for (int i = 0; i < dices.Count; i++)
        {
            int index = i;
            DiceUI diceUI = Instantiate(diceUIPrefab, transform);
            diceUI.SetDiceUI(dices[i].ElementType, dices[i].DiceNumber, () => DiceDrop(index));
            diceUIList.Add(diceUI);
        }
    }

    private void Clear()
    {
        if (games == null)
            return;

        foreach (Game game in games)
        {
            game.DiceRolled -= DiceUISetting;
        }
    }

    private void OnDestroy()
    {
        Clear();
    }
}
