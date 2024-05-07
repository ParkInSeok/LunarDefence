using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    
    public Action<Collider> triggerEnterEventHandler;
    public Action<Collider> triggerStayEventHandler;
    public Action<Collider> triggerExitEventHandler;

    public Action<Collision> collisionEnterEventHandler;
    public Action<Collision> collisionStayEventHandler;
    public Action<Collision> collisionExitEventHandler;




    protected virtual void OnTriggerEnter(Collider other)
    {
        triggerEnterEventHandler?.Invoke(other);


    }

    protected virtual void OnTriggerStay(Collider other)
    {
        triggerStayEventHandler?.Invoke(other);
        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        triggerExitEventHandler?.Invoke(other);
        
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        collisionEnterEventHandler?.Invoke(collision);
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        collisionStayEventHandler?.Invoke(collision);
        
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        collisionExitEventHandler?.Invoke(collision);
        
    }

}
