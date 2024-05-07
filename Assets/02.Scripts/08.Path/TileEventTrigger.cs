using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEventTrigger : EventTrigger
{

    public PathNode pathNode;
    public MeshRenderer meshRenderer;



    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    protected override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }
    protected override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }


}
