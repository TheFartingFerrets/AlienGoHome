using UnityEngine;
using System.Collections;

public class PhysicsEditor  : MonoBehaviour
{
    public static PhysicsEditor instance;
    public GameObject Rotator;

    public Transform Target;

    void Awake()
    {
        instance = this;
        GetComponent<CanvasGroup>().Hide();
        Rotator = GameObject.Find("PhysicsEditRotator");

    }
    public void SetTarget(Transform target)
    {
        GetComponent<CanvasGroup>().Show();
        Target = target;
    }

    public void FlipVertical()
    {
        Target.localScale = new Vector3(Target.localScale.x, -Target.localScale.y, 1);
    }
    public void FlipHorizontal()
    {
        Target.localScale = new Vector3(-Target.localScale.x, Target.localScale.y, 1);
    }

    public void Rotate()
    {
        Rotator.GetComponent<PhysicsEditRotator>().SetTarget(Target);
    }
    public void StopEditing()
    {
        Lock();
    }
    public void DestroyAsset()
    {
        GetComponent<CanvasGroup>().Hide();
        Target.GetComponent<PhysicsAsset>().DestroyAsset();
        Target = null;
    }

    public void ForceUp()
    {
        Target.GetComponent<PhysicsAsset>().AddToForce();
    }
    public void ForceDown()
    {
        Target.GetComponent<PhysicsAsset>().RemoveFromForce();
    }

    public void Lock()
    {
        GetComponent<CanvasGroup>().Hide();
        Target = null;
        Rotator.GetComponent<PhysicsEditRotator>().HideTarget();
    }
}
