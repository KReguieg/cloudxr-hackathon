﻿using System;
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
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float blastRadius;
    [SerializeField] private float explosionForce;

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
            for (int i = 0; i < collision.transform.parent.childCount; i++)
            {
                collision.transform.GetComponentInChildren<Transform>().gameObject.layer = LayerMask.NameToLayer("Default");
            }
            collision.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
            
            Explode();
            //Destroy(collision.collider.gameObject);
            Destroy(gameObject);
        }
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
