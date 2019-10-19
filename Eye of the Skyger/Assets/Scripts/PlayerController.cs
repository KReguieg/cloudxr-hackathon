using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;

    float speed;

    public float acceleration = 0.5f, maxSpeed = 2f;
    public float maxTiltAngle = 60f, tiltingSpeed = 1f;

    public float newTargetPositionTreshold = 0.03f, targetPositionReachedTreshold = 0.03f;

    Vector3 currentPosition, targetPosition, direction;

    public GameObject visual;
    [SerializeField] Transform target;

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
        if(target != null)
            newTargetPosition = target.position;
        // assign new target position if the delta is big enough
        if (Vector3.Distance(targetPosition, newTargetPosition) > newTargetPositionTreshold)
        {
            targetPosition = newTargetPosition;
        }

        // move towards target position if delta is big enough
        if (Vector3.Distance(targetPosition, currentPosition) > targetPositionReachedTreshold)
        {
            // accelerate
            if (speed < maxSpeed)
                speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);

            direction = (targetPosition - currentPosition).normalized;
            //Vector3 lookDirection = Vector3.RotateTowards(transform.forward, direction, maxTiltAngle * Mathf.Deg2Rad, 1);
            
            Vector3 deltaPos = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            //rigid.MoveRotation(Quaternion.RotateTowards(rigid.rotation, Quaternion.LookRotation(lookDirection), 
            //    tiltingSpeed * Time.deltaTime));

            //rigid.MovePosition(deltaPos);

            rigid.AddForce(direction * 25);
            
        }
        else
        {
            speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, 0, maxSpeed);
            direction = Vector3.zero;

            //rigid.MoveRotation(Quaternion.RotateTowards(rigid.rotation, Quaternion.identity, tiltingSpeed * Time.deltaTime));
        }

        Rotation();
    }

    void Rotation()
    {
    }

}
