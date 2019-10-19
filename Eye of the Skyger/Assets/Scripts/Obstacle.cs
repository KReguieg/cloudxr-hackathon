using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Rigidbody rigid;

    [Tooltip("Speed of Object in same direction as player - makes it slower")]
    public float speed = 55;

    public float gazeTime;

    public Rocket homingRocket;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = new Vector3(0, 0, -GameManager.singleton.playerSpeed + speed);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (gazeTime > GameManager.singleton.targetTime)
            GameManager.singleton.rocketTargets.Add(this);
    }

    private void OnDestroy()
    {
        if (homingRocket)
            Destroy(homingRocket.gameObject);

        GameManager.singleton.rocketTargets.Remove(this);
    }

}
