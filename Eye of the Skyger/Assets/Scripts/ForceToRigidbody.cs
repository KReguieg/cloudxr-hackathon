using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceToRigidbody : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    [SerializeField] float Force;
    [SerializeField] Vector3 Axis;
    
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody item in rigidbodies)
        {
            item.AddForce(Axis * Force);
        }
    }
}
