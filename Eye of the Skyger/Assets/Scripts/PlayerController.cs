using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rigid;

    //float speed;

    //public float acceleration = 0.5f, maxSpeed = 2f;
    public float forceMultiplier = 25f;
    public float maxRollAngle = 60f, maxPitchAngle = 40f, tiltingSpeed = 1f, tiltingNormalizationSpeed = 1f;

    public float newTargetPositionTreshold = 0.03f, targetPositionReachedTreshold = 0.03f;


    Vector3 currentPosition, targetPosition, direction;

    public GameObject visual;
    [SerializeField] Transform target;
    [SerializeField] GameObject rocketPrefab;
    [SerializeField] GameObject LockOnPrefab;
    [SerializeField] Animator multipliererEffect;

    public float cooldownDuration;
    float cooldown;
    new Collider collider;

    OneShotter oneShotter;
    float multiplier = 1;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
        oneShotter = GetComponentInChildren<OneShotter>();
    }


    // Player Input
    void Update()
    {
        // fly away at gameOver
        if (GameManager.singleton.gameOver)
        {
            rigid.velocity = new Vector3(0, 0, GameManager.singleton.playerSpeed);
            direction = Vector3.zero;
            rigid.MoveRotation(Quaternion.RotateTowards(rigid.rotation, Quaternion.identity, tiltingNormalizationSpeed * Time.deltaTime));
            return;
        }

        currentPosition = rigid.position;
        Vector3 newTargetPosition =
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.1f));
        if (target != null)
            newTargetPosition = target.position;

        // assign new target position if the delta is big enough
        if (Vector3.Distance(targetPosition, newTargetPosition) > newTargetPositionTreshold)
        {
            targetPosition = newTargetPosition;
        }

        // move towards target position if delta is big enough
        if (Vector3.Distance(targetPosition, currentPosition) > targetPositionReachedTreshold)
        {

            direction = (targetPosition - currentPosition);

            //Vector3 deltaPos = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);


            rigid.AddForce(direction * forceMultiplier);
            Rotation();
        }
        else
        {
            //speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, 0, maxSpeed);
            direction = Vector3.zero;

            rigid.MoveRotation(Quaternion.RotateTowards(rigid.rotation, Quaternion.identity, tiltingNormalizationSpeed * Time.deltaTime));
        }

        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else
            CheckTargets();

        rigid.MovePosition(transform.position + UnityEngine.Random.insideUnitSphere * multiplier * 0.01f);
    }

    private void CheckTargets()
    {
        if (GameManager.singleton.rocketTargets.Count > 0 && GameManager.singleton.rocketCount > 0)
        {
            FireRocketAt(GameManager.singleton.rocketTargets[0]);
            GameManager.singleton.rocketTargets.RemoveAt(0);
            cooldown = cooldownDuration;
        }
    }

    private void FireRocketAt(Obstacle rocketTarget)
    {
        Rocket newRocket = Instantiate(GameManager.singleton.rocketPrefab, transform.position, Quaternion.identity);
        newRocket.target = rocketTarget.transform;
        rocketTarget.homingRocket = newRocket;
    }

    void Rotation()
    {
        if (direction.x != 0)
        {
            rigid.MoveRotation(Quaternion.RotateTowards(
                rigid.rotation,
                Quaternion.Euler(-direction.normalized.y * maxPitchAngle, 0, -direction.normalized.x * maxRollAngle),
                tiltingSpeed * Time.deltaTime));
        }
    }

    public void Shoot(Transform enemy)
    {
        GameObject rocketGO = Instantiate(rocketPrefab);
        rocketGO.transform.position = transform.position;
        Rocket rocket = rocketGO.GetComponent<Rocket>();
        rocket.target = enemy;
        rocket.deepLock = true;
        Physics.IgnoreCollision(collider, rocketGO.GetComponentInChildren<Collider>());
        GameObject lockOn = Instantiate(LockOnPrefab, enemy);
        Destroy(lockOn, 2);
        oneShotter.PlaySound("RocketShoot");
    }

    private void OnTriggerExit(Collider other)
    {
        ScoreManager.Instance?.IncreaseMultiplier();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            ScoreManager.Instance.ResetMultiplier();

            oneShotter.PlaySound("ObstacleCollision");
        }
    }

    public void SetMultiplier(float value)
    {
        multipliererEffect.ForceStateNormalizedTime(value);
        Debug.Log(value);
    }

}
