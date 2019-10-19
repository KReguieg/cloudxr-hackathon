using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;

    float speed;

    public float acceleration = 0.5f, maxSpeed = 2f, maxTiltAngle = 60f;

    Vector3 currentPosition, targetPosition, direction;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();     
    }

    // Player Input
    void Update()
    {
        currentPosition = rigid.position;
        Vector3 newTargetPosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.1f));

        // assign new target position if the delta is big enough
        if (Vector3.Distance(targetPosition, newTargetPosition) > 0.03f)
        {
            targetPosition = newTargetPosition;
        }

        // move towards target position if delta is big enough
        if (Vector3.Distance(targetPosition, currentPosition) > 0.03f)
        {
            if (speed < maxSpeed)
                speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);

            Vector3 deltaPos = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            
            rigid.MovePosition(deltaPos);
        }
        else
            speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, 0, maxSpeed); ;
    }


}
