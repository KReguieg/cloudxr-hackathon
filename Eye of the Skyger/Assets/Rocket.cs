using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Obstacle target;
    Rigidbody rigid;
    public float forceMultiplier;

    public float acceleration = 1;
    float speed = 0;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        speed += acceleration * Time.deltaTime;

        rigid.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime));

        rigid.MoveRotation(Quaternion.LookRotation(target.transform.position));
        //rigid.AddForce(transform.forward * forceMultiplier, ForceMode.Acceleration);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            SpawnExplosion();
            Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
    }

    private void SpawnExplosion()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}
