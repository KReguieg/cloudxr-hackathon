using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    [SerializeField] Transform linkedTransform;
    [SerializeField] AudioSource lightleftDirection;
    [SerializeField] AnimationCurve lightAngleVolumeCurve;
    [SerializeField] AudioSource leftDirection;
    [SerializeField] AnimationCurve angleVolumeCurve;

    void Update()
    {
        Vector3 look = linkedTransform.forward;
        look.y = 0;
        float angley = Mathf.Abs(Vector3.SignedAngle(look, Vector3.forward, Vector3.right));
        leftDirection.volume = angleVolumeCurve.Evaluate(angley);
        lightleftDirection.volume = lightAngleVolumeCurve.Evaluate(angley);
    }
}
