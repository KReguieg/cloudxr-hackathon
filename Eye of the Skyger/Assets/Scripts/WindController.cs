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
        /* blend windsounds depending on
        where is the linked transform looking in the horizontal axis */

        Vector3 horizontalLook = linkedTransform.forward;
        horizontalLook.y = 0;

        float angleY = Vector3.Angle(horizontalLook, Vector3.forward);
        leftDirection.volume = angleVolumeCurve.Evaluate(angleY);
        lightleftDirection.volume = lightAngleVolumeCurve.Evaluate(angleY);
    }
}
