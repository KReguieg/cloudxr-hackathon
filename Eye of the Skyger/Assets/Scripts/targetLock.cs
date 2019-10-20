using UnityEngine;

public class targetLock : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.position - transform.position);
    }
}
