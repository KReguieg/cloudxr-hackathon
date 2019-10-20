using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventOnGaze : ObjectToGaze
{
    [SerializeField] float activateAfterTimer = 0.3f;
    [SerializeField] float triggerDistance;
    public UnityEvent OnEnterTriggerdEvent;
    [SerializeField] UnityEvent DelayedTriggerdEvent;

    [SerializeField] UnityEvent OnLeaveTriggerEvent;
    float timer = 0;
    bool LockOn;
    bool triggerd = false;
    Material material;
    bool oldLockOn;

    private void Start()
    {
        
    }

    public override void GazeAt(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) <= triggerDistance)
        {
            LockOn = true;
        }
        else
        {
            LockOn = false;
        }
        
    }

    private void Update()
    {
        if (LockOn && !triggerd)
        {
            timer += Time.deltaTime;
            if (timer >= triggerDistance)
            {
                triggerd = true;
                DelayedTriggerdEvent?.Invoke();
            }
        }
        if (oldLockOn && !LockOn)
        {
            OnLeaveTriggerEvent?.Invoke();
        }
        if (!oldLockOn && LockOn)
        {
            OnEnterTriggerdEvent?.Invoke();
        }

        oldLockOn = LockOn;
    }
}
