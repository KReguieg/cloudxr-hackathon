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
        if (gazeTime > GameManager.singleton.targetTime)
            GameManager.singleton.rocketTargets.Add(this);
    }

    private void OnDestroy()
    {
        if (homingRocket)
            Destroy(homingRocket.gameObject);

        GameManager.singleton.rocketTargets.Remove(this);
    }

}
