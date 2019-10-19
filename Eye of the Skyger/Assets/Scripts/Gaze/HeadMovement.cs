using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve angleSpeedCurve;
    [SerializeField] Transform controlledTransfrom;
    [SerializeField] float maxDistance;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = controlledTransfrom.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 look = transform.forward;
        look.y = 0;
        float anglex = Vector3.SignedAngle(look, Vector3.forward, Vector3.up);
        controlledTransfrom.position += Vector3.right * angleSpeedCurve.Evaluate(-anglex) * Time.deltaTime;
        look = transform.forward;
        look.x = 0;
        float angley = Vector3.SignedAngle(look, Vector3.forward, Vector3.right);
        controlledTransfrom.position += Vector3.up * angleSpeedCurve.Evaluate(angley) * Time.deltaTime;

        Vector3 clamed = Vector3.ClampMagnitude(controlledTransfrom.position, maxDistance);
        controlledTransfrom.position = clamed;
    }
}
