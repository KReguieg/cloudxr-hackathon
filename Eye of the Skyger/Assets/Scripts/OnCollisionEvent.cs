using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnCollisionEvent : MonoBehaviour
{
    [SerializeField] UnityEvent triggerdEvent;
    private void OnCollisionEnter(Collision other) {
        triggerdEvent.Invoke();
    }
}
