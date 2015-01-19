using UnityEngine;
using System.Collections;

public class PhysicsSpring : MonoBehaviour 
{
    float ForceMulti = 60f;
    
    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PhysicsRoller")
        {
            float Force = ForceMulti * GetComponentInParent<PhysicsAsset>().PhysicsForce;
            Debug.Log(Force);
            coll.gameObject.rigidbody2D.AddForce(transform.up * Force);
        }
    }
}
