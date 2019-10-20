using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    private const int percentageOfDestructables = 3;
    [SerializeField] private int minCols = 6;
    [SerializeField] private int maxCols = 15;
    [SerializeField] private float positionOffset = 0.1f;
    [SerializeField] private float size = 10f;
    [SerializeField] private int objRows;
    [SerializeField] private int objCols;
    [SerializeField] private GameObject[] prefabToSpawn;
    [SerializeField] private Material targetMaterial;
    private float offset;

    private Vector3[] axis = new Vector3[]{Vector3.right, Vector3.up, Vector3.forward};
    private float[] rotations = new float[]{0, 90, 180, 270};

    private void Awake() {
        objCols = Random.Range(minCols, maxCols);
        offset = size + positionOffset;
        for (int x = 0; x < objRows; x++)
        {
            for (int y = 0; y < objCols; y++)
            {
                var position = new Vector3(transform.position.x + offset * x, transform.position.y + offset * y);
                var g = Instantiate(prefabToSpawn[Random.Range(0, prefabToSpawn.Length)], position, Quaternion.identity);
                g.transform.Rotate(axis[Random.Range(0, axis.Length)], rotations[(Random.Range(0, rotations.Length))]);
                g.transform.localScale = Vector3.one * size;
                g.transform.parent = transform;
                g.tag = "Obstacle";
                g.AddComponent<BoxCollider>();
                g.AddComponent<Rigidbody>();
                g.GetComponent<Rigidbody>().useGravity = false;
                g.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                g.AddComponent<MovingObject>();
            }
        }
        SelectRandomAndMakeDestructable();
    }

    private void SelectRandomAndMakeDestructable()
    {
        GameObject[] Nodes = transform.GetComponentsInChildren<GameObject>();
        var randomObjects = new GameObject[(int)Mathf.Floor(objCols * (1 / percentageOfDestructables))]; 

        for(int i = 0; i < objCols; i++) {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, Nodes.Length);
            randomObjects[i] = Nodes[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            Nodes[take] = Nodes[i];
            Nodes[i] = randomObjects[i];
        }

        for (int i = 0; i < randomObjects.Length; i++)
        {
            randomObjects[i].GetComponentInChildren<MeshRenderer>().material = targetMaterial;
            randomObjects[i].AddComponent<Obstacle>();
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
