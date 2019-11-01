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
    [SerializeField] float maxPitchAngle = 50;
    [SerializeField] float maxRollAngle = 80;
    [SerializeField] float tiltingSpeed = 200;
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] GameObject ShipDeberis;
    [SerializeField] float ExplosionForce = 10;
    public Transform playerLink;
    new Collider collider;

    float startOffset;
    float forward;
    float shootTimer = 0;
    private float rocketSpeed = 1;
    Vector3 startPosition;
    Rigidbody rigid;
    Vector3 lastPosition;

    OneShotter oneShotter;

    private void Awake()
    {
        oneShotter = GetComponentInChildren<OneShotter>();
    }

    void Start()
    {
        startOffset = Random.value * Mathf.PI * 2;
        radius += Random.value * radiusVarianz - radiusVarianz / 2;
        rotationSpeed += Random.value * 1f - 0.5f;
        if (Random.value <= 0.5f)
            rotationSpeed *= -1;
        collider = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        startPosition = transform.position;
        SetPOsition();
    }

    void SetPOsition()
    {
        lastPosition = transform.position;
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * rotationSpeed + startOffset) * radius,
                                                         Mathf.Cos(Time.time * rotationSpeed + startOffset) * radius, forward);

    }

    void Update()
    {
        if (forward < maxForward)
            forward += Time.deltaTime * 2;
        SetPOsition();
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

            oneShotter.PlaySound("ShootRocket");
        }
        Rotation();
    }

    public void TriggerShoot()
    {
        playerLink.GetComponentInChildren<PlayerController>().Shoot(transform);
    }
    void Rotation()
    {
        Vector3 direction = transform.position - lastPosition;
        if (direction.x != 0)
        {
            rigid.MoveRotation(Quaternion.RotateTowards(
                rigid.rotation,
                Quaternion.Euler(-direction.normalized.y * maxPitchAngle, 0, -direction.normalized.x * maxRollAngle),
                tiltingSpeed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider.tag);
        if (other.collider.tag == "Rocket")
        {

            ScoreManager.Instance.IncreaseMultiplier(1);
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(Instantiate(ExplosionEffect, transform.position, Quaternion.identity), 2);
        GameObject debris = Instantiate(ShipDeberis, transform.position, Quaternion.identity);
        foreach (Rigidbody rigidbody in debris.GetComponentsInChildren<Rigidbody>())
            rigidbody.AddExplosionForce(ExplosionForce, transform.position, 1);
        Destroy(debris, 10);
    }
}
