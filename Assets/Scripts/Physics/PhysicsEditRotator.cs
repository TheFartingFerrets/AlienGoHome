using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PhysicsEditRotator : MonoBehaviour 
{
    [SerializeField]
    Transform Target;
    Transform Rotator;
    LineRenderer RotateRender;
    [SerializeField]
    Vector3 RotatorPos;

    void Awake()
    {
        Rotator = transform.GetChild(0);
        RotateRender = Rotator.GetComponent<LineRenderer>();

        RotateRender.SetPosition(0, transform.position);
        RotateRender.SetPosition(1, Rotator.position);

        HideTarget();

    }

    void Update()
    {
        if( Target )
        {
            RotateRender.SetPosition(0, transform.position);
            RotateRender.SetPosition(1, Rotator.position);
            transform.position = Target.position;
            transform.rotation = Target.rotation;
        }
        else
        {
            RotateRender.SetPosition(0, transform.position);
            RotateRender.SetPosition(1, transform.position);
        }
            
    }

    public void SetTarget(Transform _Target)
    {
        Target = _Target;
    }
    public void HideTarget()
    {
        Target = null;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-0, -1, 0)) + Vector3.down * 10f;
        transform.position = transform.position - Vector3.forward * -10f;

        Rotator.localPosition = Vector3.right;
    }

    public void MouseDown()
    {
        RotatorPos = Target.position;
    }

    public void MouseDrag()
    {
        if( Target )
        {   
            Vector3 RotationPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RotationPosition.z = transform.position.z;

            Vector3 dir = Target.position - RotationPosition;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            //angle = Mathf.Round(angle / 10f) * 10f;

            Target.rotation = Quaternion.AngleAxis(-(angle + 90f), Vector3.forward);
            
            Vector3 allowedDistance = RotationPosition - RotatorPos;
            allowedDistance = Vector3.ClampMagnitude(allowedDistance, 2f);
            Rotator.position = RotatorPos + allowedDistance;
        }
    }


}
