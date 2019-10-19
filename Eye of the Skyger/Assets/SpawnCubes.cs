using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private float size = 10f;
    [SerializeField] private int objRows;
    [SerializeField] private int objCols;
    [SerializeField] private GameObject prefabToSpawn;
    private float offset;

    private Vector3[] axis = new Vector3[]{Vector3.right, Vector3.up, Vector3.forward};
    private float[] rotations = new float[]{0, 90, 180, 270};


    private void Awake() {
        objCols = Random.Range(6, 15);
        offset = size + 0.1f;
        for (int x = 0; x < objRows; x++)
        {
            for (int y = 0; y < objCols; y++)
            {
                var position = new Vector3(transform.position.x + offset * x, transform.position.y + offset * y);
                var g = Instantiate(prefabToSpawn, position, Quaternion.identity);
                g.transform.Rotate(axis[Random.Range(0, axis.Length)], rotations[(Random.Range(0, rotations.Length))]);
                g.transform.localScale = Vector3.one * size;
                g.transform.parent = transform;
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
