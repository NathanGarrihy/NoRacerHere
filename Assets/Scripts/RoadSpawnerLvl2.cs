using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawnerLvl2 : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 50f;

    // Start is called before the first frame update
    void Start()
    {
        // sort list of roads to keep consistant order
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    // Move first road from the back to the front
    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        movedRoad.transform.position = new Vector3(0, 5, newZ);
        roads.Add(movedRoad);
    }
}
