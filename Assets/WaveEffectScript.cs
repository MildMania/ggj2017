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
    public float MinAmount, MaxAmount;
    void OnEnable()
    {
        MakeWaveEffect();
    }

    void MakeWaveEffect()
    {
        int lastIndex = -1;
        foreach (var renderer in RenderersToMove)
        {
            int index = -1;

            do
            {
                index = UnityEngine.Random.Range(0, 2);
            }
            while (lastIndex == index);

            lastIndex = index;

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

            float newAmount = UnityEngine.Random.Range(MinAmount, MaxAmount);
            renderer.transform.DOLocalMoveZ((int)dir * newAmount, Duration).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
