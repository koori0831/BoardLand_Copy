using Alchemy.Inspector;
using Core;
using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;

public record struct StartOpeningAnimationEvent(List<Transform> Tiles) : IEvent;
public class OpeningAnimation : MonoBehaviour
{
    [SerializeField] private Transform tileLowPos, tileHighPos;
    [SerializeField] private bool debugMode = false;
    [ShowIf(nameof(debugMode))][SerializeField] private List<Transform> debugTiles;
    private float tileLowPosY => tileLowPos.position.y;
    private float tileHighPosY => tileHighPos.position.y;
    private List<Transform> tiles = new();
    private bool isAnimationRunning = false;
    private int currentIndex = -1;

    private void Awake()
    {
        EventBus<StartOpeningAnimationEvent>.Subscribe(OnRequestStartAnimaiton);
    }

    private void OnRequestStartAnimaiton(StartOpeningAnimationEvent evt)
    {
        if (isAnimationRunning)
            return;

        isAnimationRunning = true;
        foreach (Transform tile in evt.Tiles)
            tile.position.Set(tile.position.x, tileHighPosY, tile.position.z);

        tiles = evt.Tiles;
        StartAnimation();
    }

    private void StartAnimation()
    {
        currentIndex++;
        if (currentIndex >= tiles.Count)
        {
            isAnimationRunning = false;
            return;
        }

        LMotion.Create(tileHighPosY, tileLowPosY, 0.1f)
            .WithEase(Ease.OutExpo)
            .WithOnComplete(StartAnimation)
            .BindToPositionY(tiles[currentIndex])
            .AddTo(this);
    }

    private void OnDestroy()
    {
        EventBus<StartOpeningAnimationEvent>.Unsubscribe(OnRequestStartAnimaiton);
    }

    [ShowIf(nameof(debugMode))]
    [LabelText("테스트용 이벤트 발행")]
    [Button]
    private void AnimationTest()
    {
        EventBus<StartOpeningAnimationEvent>.Publish(new(debugTiles));
    }
}
