using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Animator animator;
    [SerializeField] private SerializableDictionary<EElementType, SpriteList> diceImages;
    [SerializeField] private SerializableDictionary<EElementType, string> diceAnimations; // stateName
    [SerializeField] private float defaultRollTime = 2.0f;

    public async UniTask StartAnimation(EElementType elementType, int diceNumber, int diceIndex)
    {
        await PlayUntilTimeAsync(diceAnimations[elementType], (defaultRollTime + diceIndex) / 2);
        image.sprite = diceImages[elementType].sprites[diceNumber - 1];
    }

    public async UniTask PlayUntilTimeAsync(string stateName, float targetSeconds)
    {
        animator.Play(stateName);

        await UniTask.DelayFrame(1);

        float elapsedTime = 0f;
        while (elapsedTime < targetSeconds)
        {
            elapsedTime += Time.deltaTime;
        }

        animator.speed = 0f;
    }
}
