using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRocket : ObjectToGaze
{
    float TriggerDistance = 2;
    public override void GazeAt(Vector3 position)
    {
        if (Vector3.Distance(transform.position, position) <= TriggerDistance)
            PlayerController.instance.Shoot(transform);
    }
}
