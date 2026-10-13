using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DiceRollPopupUI : MonoBehaviour
{
    [SerializeField] DiceRollAnimation diceAnimationPrefab;
    [SerializeField] Transform layoutParent;
    private bool isAnimaitonPlaying;

    public async Task DrawAnimations(List<Dice> dices)
    {
        isAnimaitonPlaying = true;
        foreach (Transform child in layoutParent)
            Destroy(child.gameObject);

        for (int i = 0; i < dices.Count; i++)
        {
            await Instantiate(diceAnimationPrefab, layoutParent).StartAnimation(dices[i].ElementType, dices[i].DiceNumber, i);
        }
        isAnimaitonPlaying = false;
    }

    public void ClosePopup()
    {
        if (isAnimaitonPlaying)
            return;
        gameObject.SetActive(false);
    }
}
