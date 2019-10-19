using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToGaze : MonoBehaviour, IGazeable
{
    public virtual void GazeAt(Vector3 position)
    {
        transform.position = position;
    }
}
