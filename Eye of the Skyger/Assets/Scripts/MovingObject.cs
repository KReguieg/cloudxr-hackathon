using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    protected Rigidbody rigid;

    [Tooltip("Speed of Object in same direction as player - makes it slower")]
    public float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.back * (GameManager.singleton.playerSpeed + speed);
        // rigid.velocity = new Vector3(0, 0, -GameManager.singleton.playerSpeed + speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
