using UnityEngine;
using System.Collections;

public class SimpleDrag : MonoBehaviour 
{

    public void OnMouseDown()
    {
        transform.parent.SendMessage("MouseDown");
    }
    public void OnMouseDrag()
    {
        transform.parent.SendMessage("MouseDrag");
    }

}
