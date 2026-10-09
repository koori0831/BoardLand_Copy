using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : UnitRenderer
{
    [SerializeField] private float stepDuration = 0.2f;

    private MotionHandle moveMotion;
    private List<Vector3> path = new();
    private int pathIndex;

    public void Teleport(Vector3 position)
    {
        StopMove();
        transform.position = position;
    }

    // path에 담긴 위치들을 순서대로 한 칸씩 방문한다.
    public void Move(List<Vector3> path)
    {
        StopMove();
        this.path.AddRange(path);
        MoveNextStep();
    }

    private void MoveNextStep()
    {
        if (pathIndex >= path.Count)
        {
            StopMove();
            return;
        }

        Vector3 target = path[pathIndex++];
        moveMotion = LMotion.Create(transform.position, target, stepDuration)
            .WithEase(Ease.OutQuad)
            .WithOnComplete(MoveNextStep)
            .BindToPosition(transform)
            .AddTo(this);
    }

    private void StopMove()
    {
        moveMotion.TryCancel();
        path.Clear();
        pathIndex = 0;
    }
}
