using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DiceRollPopupUI : MonoBehaviour
{
    [SerializeField] DiceRollAnimation diceAnimationPrefab;
    [SerializeField] Transform layoutParent;
    private bool isAnimaitonPlaying;

    public async UniTask PlayDiceRollAnimations(List<Dice> dices)
    {
        isAnimaitonPlaying = true;
        foreach (Transform child in layoutParent)
            Destroy(child.gameObject);

        List<UniTask> tasks = new List<UniTask>(dices.Count);
        for (int i = 0; i < dices.Count; i++)
        {
            tasks.Add(Instantiate(diceAnimationPrefab, layoutParent)
                .StartAnimation(dices[i].ElementType, dices[i].DiceNumber, i));
        }
        await UniTask.WhenAll(tasks);
        isAnimaitonPlaying = false;
    }

    public void ClosePopup()
    {
        if (isAnimaitonPlaying)
            return;
        gameObject.SetActive(false);
    }
}
