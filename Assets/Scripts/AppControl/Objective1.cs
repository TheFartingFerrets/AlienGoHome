using UnityEngine;
using System.Collections;

[System.Serializable]
public enum ObjType
{
    LessThan,
    MoreThan,
    Time,
    Collected
}
[System.Serializable]
public class Objective1
{
    [HideInInspector]
    public string Name = "Objective";
    public bool IsCompleted = false;
    public ObjType RequirementType = ObjType.LessThan;
    public float Requirement;

    public bool CheckResult(int _Amount)
    {
        switch (RequirementType)
        {
            case ObjType.LessThan:
                if (_Amount <= Requirement)
                    IsCompleted = true;
                else
                    IsCompleted = false;
                break;
            case ObjType.MoreThan:
                if (_Amount >= Requirement)
                    IsCompleted = true;
                else
                    IsCompleted = false;
                break;
        }
        return IsCompleted;
    }

    public bool CheckResult(float _Time)
    {
        if (_Time < Requirement)
            IsCompleted = true;
        else
            IsCompleted = false;

        return IsCompleted;
    }
    public bool CheckResult(bool _Collected)
    {
        if (_Collected)
            IsCompleted = true;
        else
            IsCompleted = false;
        return IsCompleted;
    }
}
