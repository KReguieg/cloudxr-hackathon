using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingObject
{
    

    public float gazeTime;

    public Rocket homingRocket;


    // Update is called once per frame
    void Update()
    {
        if (gazeTime > GameManager.instance.targetTime)
            GameManager.instance.rocketTargets.Add(this);
    }

    private void OnDestroy()
    {
        if (homingRocket)
            Destroy(homingRocket.gameObject);

        GameManager.instance.rocketTargets.Remove(this);
    }

}
