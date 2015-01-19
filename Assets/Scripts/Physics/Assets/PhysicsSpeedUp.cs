using UnityEngine;
using System.Collections;

public class PhysicsSpeedUp : MonoBehaviour
{
    Vector2 Direction;
    float ForceMulti = 10f;

    void Start()
    {
        Direction = transform.right;
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * transform.parent.localScale.x, Color.blue);
    }
    public void ChangeDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PhysicsRoller")
        {
            float Force = ForceMulti * GetComponentInParent<PhysicsAsset>().PhysicsForce;
            Debug.Log(Force);
            other.rigidbody2D.AddForce(transform.right * transform.parent.localScale.x * Force);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PhysicsRoller")
        {
            //Rotate the roller to face the same rotation
        }
    }
}
