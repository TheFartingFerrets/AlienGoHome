using UnityEngine;
using System.Collections;

public class ObjectiveManager : MonoBehaviour 
{
    public Objective _Objective_1;
    public Objective _Objective_2;
    public Objective _Objective_3;
}

public enum ObjectiveType
{
    LessThanReq,
    MoreThanReq,
    Bonus,
}

[System.Serializable]
public class Objective
{
    [SerializeField]
    float Requirement = 0;
    [SerializeField]
    ObjectiveType ObjType;
    [SerializeField]
    private bool isComplete = false;

    public bool CheckObjective(float _ToCheck)
    {
        switch( ObjType )
        {
            case ObjectiveType.LessThanReq:
                isComplete = _ToCheck < Requirement ? true : false;
                break;
            case ObjectiveType.MoreThanReq:
                isComplete = _ToCheck >= Requirement ? true : false;
                break;
        }
        Debug.Log(isComplete);
        return isComplete;
    }
    public bool CheckObjective( bool _Bonus)
    {
        isComplete = _Bonus == true ? true : false;
        Debug.Log(isComplete);
        return isComplete;
    }
    public bool GetValue()
    {
        return isComplete;
    }

}

