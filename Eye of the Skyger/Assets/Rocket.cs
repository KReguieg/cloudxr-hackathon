using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float startSpeed = 2;
    [SerializeField] float looseFocusAfterTime = 0.25f;
    public Transform target;
    Rigidbody rigid;
    public float forceMultiplier;

    public float acceleration = 1;
    float speed = 0;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float blastRadius;
    [SerializeField] private float explosionForce;
    public bool deepLock; // for the Rockets from the player to always hit
    float timer = 0;
    Vector3 lastDirection;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        speed = startSpeed;
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!deepLock)
            timer += Time.deltaTime;
        speed += acceleration * Time.deltaTime;
        if (timer <= looseFocusAfterTime)
        {
            if(target != null){
                lastDirection = target.position - transform.position;
            }else
            {
                lastDirection = transform.forward;
            }
            rigid.MovePosition(Vector3.MoveTowards(transform.position, transform.position + lastDirection, speed * Time.deltaTime));
            rigid.MoveRotation(Quaternion.LookRotation(lastDirection));
        }
        else
            rigid.MovePosition(Vector3.MoveTowards(transform.position, transform.position + lastDirection.normalized, speed * Time.deltaTime));
        //rigid.AddForce(transform.forward * forceMultiplier, ForceMode.Acceleration);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            for (int i = 0; i < collision.transform.parent.childCount; i++)
            {
                collision.transform.GetComponentInChildren<Transform>().gameObject.layer = LayerMask.NameToLayer("Default");
            }
            collision.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        Explode();
        //Destroy(collision.collider.gameObject);
        Destroy(gameObject);
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] nearbyObjectsToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (var nearbyObject in nearbyObjectsToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }
    }
}
