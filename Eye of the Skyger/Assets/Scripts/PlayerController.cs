using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigid;



    Vector3 currentPosition, targetPosition;

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
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11));

        if (Vector3.Distance(targetPosition, newTargetPosition) > 0.5f)
        {
            targetPosition = newTargetPosition;
        }

        if (Vector3.Distance(targetPosition, currentPosition) > 0.5f)
        {
            rigid.MovePosition(targetPosition);
            //rigid.AddForce(targetPosition - currentPosition, ForceMode.Acceleration);
        }
    }


}
