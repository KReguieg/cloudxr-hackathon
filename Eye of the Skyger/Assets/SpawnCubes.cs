using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private float size = 10f;
    [SerializeField] private int objRows;
    [SerializeField] private int objCols;
    private float offset;

    private void Awake() {
        offset = size + 0.02f;
        for (int x = 0; x < objRows; x++)
        {
            for (int y = 0; y < objCols; y++)
            {
                var g = GameObject.CreatePrimitive(PrimitiveType.Cube);
                g.transform.localScale = Vector3.one * size;
                g.transform.parent = transform;
                g.transform.position = new Vector3(transform.position.x + offset * x, transform.position.y + offset * y);
                g.transform.rotation =  Quaternion.identity;
                g.AddComponent<BoxCollider>();
                g.AddComponent<Rigidbody>();
                g.GetComponent<Rigidbody>().useGravity = false;
                g.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                g.AddComponent<MovingObject>();
                if (x == 1 & y == 1)
                {
                    g.AddComponent<Obstacle>();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
