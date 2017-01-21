using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum DirectionToMove
{
    Forward = 1,
    Backward = -1
}

public class WaveEffectScript : MonoBehaviour
{
    public List<SpriteRenderer> RenderersToMove;
    public float MoveAmount, Duration;

    void OnEnable()
    {
        MakeWaveEffect();
    }

    void MakeWaveEffect()
    {
        foreach (var renderer in RenderersToMove)
        {
            int index = UnityEngine.Random.Range(0, 2);

            DirectionToMove dir = DirectionToMove.Forward;
            switch (index)
            {
                case 0:
                    dir = DirectionToMove.Forward;
                    break;
                case 1:
                    dir = DirectionToMove.Backward;
                    break;
            }

            renderer.transform.DOLocalMoveX(renderer.transform.localPosition.x + ((int)dir * MoveAmount), Duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
