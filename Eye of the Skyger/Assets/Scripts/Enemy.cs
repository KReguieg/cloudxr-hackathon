using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float maxForward = 4;
    [SerializeField] float radius = 3;
    [SerializeField] float radiusVarianz = 1;
    [SerializeField] float fireSpeed = 3;
    [SerializeField] GameObject RocketPrefab;
    public Transform playerLink;
    new Collider collider;

    float startOffset;
    float forward;
    float shootTimer = 0;
    private float rocketSpeed = 1;
    Vector3 startPosition;

    
    void Start()
    {
        startOffset = Random.value * Mathf.PI * 2;
        radius += Random.value * radiusVarianz - radiusVarianz / 2;
        rotationSpeed += Random.value * 1f - 0.5f;
        if (Random.value <= 0.5f)
            rotationSpeed *= -1;
        collider = GetComponent<Collider>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (forward < maxForward)
            forward += Time.deltaTime * 2;
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * rotationSpeed + startOffset) * radius,
                                                        Mathf.Cos(Time.time * rotationSpeed + startOffset) * radius, forward);

        shootTimer += Time.deltaTime;
        if (shootTimer >= fireSpeed)
        {
            shootTimer = 0;
            GameObject rocketGO = Instantiate(RocketPrefab);
            
            rocketGO.transform.position = transform.position;
            Rocket rocket = rocketGO.GetComponent<Rocket>();
            rocket.target = playerLink;
            rocket.startSpeed = rocketSpeed;
            Physics.IgnoreCollision(collider, rocketGO.GetComponentInChildren<Collider>());
        }
    }

    public void TriggerShoot()
    {
        playerLink.GetComponentInChildren<PlayerController>().Shoot(transform);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
