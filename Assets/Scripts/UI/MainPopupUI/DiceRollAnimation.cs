using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollAnimation : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Animator animator;
    [SerializeField] private SerializableDictionary<EElementType, SpriteList> diceImages;
    [SerializeField] private SerializableDictionary<EElementType, string> diceAnimations; // stateName
    [SerializeField] private float defaultRollTime = 1.0f;

    public async UniTask StartAnimation(EElementType elementType, int diceNumber, int diceIndex)
    {
        await PlayForSecondsAsync(diceAnimations[elementType], (defaultRollTime + diceIndex) / 2f);
        image.sprite = diceImages[elementType].sprites[diceNumber - 1];
    }

    private async UniTask PlayForSecondsAsync(string stateName, float seconds)
    {
        animator.enabled = true;
        animator.speed = 1f;
        animator.Play(stateName, 0, 0f);

        await UniTask.Delay(TimeSpan.FromSeconds(seconds));

        animator.enabled = false;
    }
}
