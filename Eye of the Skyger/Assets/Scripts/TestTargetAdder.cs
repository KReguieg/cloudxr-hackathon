using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTargetAdder : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            GameManager.singleton.rocketTargets.Add(other.GetComponent<Obstacle>());
    }
}
