using UnityEngine;
using System;
using System.Collections;
using TouchScript.Gestures;


public class PhysicsAsset : MonoBehaviour 
{
    public bool isLocked = false;
    private Vector2 DragOffset = Vector2.zero;
    [SerializeField]
    GameObject DrawParent;

    public float PhysicsForce = 1f;

    public void SetAssetDrawParent(GameObject _DrawParent)
    {
        DrawParent = _DrawParent;
    }

    public void AddToForce()
    {
        PhysicsForce++;
        PhysicsForce = Mathf.Clamp(PhysicsForce, 0, 100);
    }
    public void RemoveFromForce()
    {
        PhysicsForce--;
        PhysicsForce = Mathf.Clamp(PhysicsForce, 0, 100);
    }

    void Update()
    {
        if( !isLocked )
        {
            //allow drag
        }
    }
    public void Lock()
    {
        isLocked = true;
    }
    public void Unlock()
    {
        isLocked = false;
    }

    public void OnMouseDown()
    {
        if( !isLocked)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DragOffset = touchPosition - rigidbody2D.position;
        }
    }
    public void OnMouseDrag()
    {
        if( !isLocked)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rigidbody2D.MovePosition(touchPosition - DragOffset);
        }

    }
    public void OnMouseUp()
    {
        DragOffset = Vector2.zero;
    }

    public void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }

    public void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    public void tappedHandler(object sender, EventArgs e)
    {
        if(!isLocked)
        {
            if( PhysicsEditor.instance.Target == this.transform)
            {
                PhysicsEditor.instance.Lock();
            }
            else
            {
                PhysicsEditor.instance.SetTarget(this.transform);
            }
        }
    }

    public void DestroyAsset()
    {
        Debug.Log("Destroy");
        if (DrawParent)
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
            DrawParent.GetComponent<PhysicsDrawAsset>().PrefabAmount++;
            PhysicsControl.control.AssetsInScene--;
        }
    }


}
