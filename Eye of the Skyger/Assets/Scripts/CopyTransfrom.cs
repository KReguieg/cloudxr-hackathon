using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransfrom : MonoBehaviour
{
    [SerializeField] Transform trackedTransform;
    Vector3 start;
    private void Start() {
        start = transform.position;
    }

    void Update()
    {
        Vector3 pos = start;
        pos.y += trackedTransform.position.y;
         transform.position = pos;
    }
}
