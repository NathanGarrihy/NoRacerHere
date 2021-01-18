using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollectableSpace : MonoBehaviour
{
    public List<float> collectableLanesX;

    public float GetLane()
    {
        if (collectableLanesX == null || collectableLanesX.Count < 1)
        {
            return -30f;
        }
        return collectableLanesX[Random.Range(0, collectableLanesX.Count)];
    }

}
