using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            Destroy(other.gameObject);
        }
    }
}