using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] UnityEvent triggerdEvent;
    [SerializeField] bool useTrigger = false;
    private void OnCollisionEnter(Collision other)
    {
        triggerdEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(useTrigger)
            triggerdEvent.Invoke();
    }
}
