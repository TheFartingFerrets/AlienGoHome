using UnityEngine;
using System.Collections;

public class DeleteThis : MonoBehaviour 
{



    public void OnGUI()
    {
        if( GUILayout.Button("Complete Level"))
        {
            Complete();
        }
    }

    void Complete()
    {
        //AppControl.control.UpdateObjective(0, true);
        //AppControl.control.UpdateObjective(1, true);
        //AppControl.control.UpdateObjective(2, true);
        int[] obj = { 0, 1, 2 };
        bool[] objStatus = { true, true, false };
        //AppControl.control.UpdateObjective(obj, objStatus);

        //AppControl.control.UpdateObjective(1, 0, 0, true);

        //AppControl.control.UpdateObjective(1, 0, obj, objStatus);
    }
}
