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
    [SerializeField] private List<RectTransform> weaponArea;
    [SerializeField] private WeaponInfoUI weaponInfoUI;
    [SerializeField] private Camera uiCamera;
    private List<DiceUI> diceUIList = new();
    private List<Game> games = new();
    private Board board;

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
        foreach (RectTransform area in weaponArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Mouse.current.position.ReadValue(), uiCamera))
                return weaponInfoUI.HasWeapon ? EPlayerInputType.Weapon : EPlayerInputType.Cancel;
        }
        foreach (RectTransform area in actionArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Mouse.current.position.ReadValue(), uiCamera))
                // 이번 턴에 이미 행동한 타일이면 취소 처리
                return board.Games[board.CurrentGameIndex].CanTileAction ? EPlayerInputType.Action : EPlayerInputType.Cancel;
        }
        foreach (RectTransform area in cancelArea)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(area, Mouse.current.position.ReadValue(), uiCamera))
                return EPlayerInputType.Cancel;
        }
        return EPlayerInputType.Move;
    }

    public void Bind(Board board)
    {
        Clear();
        this.board = board;
        games = board.Games;
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
        // 씬이 넘어가 파괴된 경우에는 대기를 취소한다
        await UniTask.WaitUntil(() => diceRollPopupUI.gameObject.activeSelf == false, cancellationToken: this.GetCancellationTokenOnDestroy());
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
