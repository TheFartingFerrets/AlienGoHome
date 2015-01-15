using UnityEngine;
using System.Collections;

public class Objective
{
    public bool IsCompleted = false;
    public int requirement;

    public void CheckRequirement( int amount )
    {
        if (amount == requirement)
            IsCompleted = true;
        else
            IsCompleted = false;
    }
}
