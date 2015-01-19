using UnityEngine;
using System.Collections;

public class PhysicsPortal : MonoBehaviour 
{
    public Transform PortalOut;

    public void SetPortalOut( Transform _PortalOut)
    {
        PortalOut = _PortalOut;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PhysicsRoller")
        {
            other.rigidbody2D.position = PortalOut.position + Vector3.right * 0.2f;
            other.rigidbody2D.velocity = PortalOut.right * other.rigidbody2D.velocity.magnitude;
        }
    }

}
