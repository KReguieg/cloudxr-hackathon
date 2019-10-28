using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Cloud"))
            Destroy(other.gameObject);
    }
}
