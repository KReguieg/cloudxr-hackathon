using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody rigid;

    public float speed = 180;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("hello");
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
