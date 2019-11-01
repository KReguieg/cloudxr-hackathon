using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerShooter : MonoBehaviour
{
    [SerializeField] LayerMask raycastLayer;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform ShootDirection;
    [SerializeField] float ShootSpeed = 1;
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        RaycastHit hit;
        lineRenderer.SetPosition(0, transform.position);
        if (Physics.Raycast(transform.position, ShootDirection.forward, out hit, 100, raycastLayer))
        {
            lineRenderer.SetPosition(1, hit.point);
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null && timer >= ShootSpeed)
            {
                enemy.TriggerShoot();
                timer = 0;
            }
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + ShootDirection.forward * 100);
        }
    }
}
