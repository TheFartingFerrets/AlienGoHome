using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level
{
    [HideInInspector]
    public string Name = "Level";
    [SerializeField]
    public bool isLocked = true;
    //[SerializeField]
    //public Objective[] Objectives = new Objective[3];

    public bool Objective_1 = false;
    public bool Objective_2 = false;
    public bool Objective_3 = false;
}
