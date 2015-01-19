using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObjectiveManager))]
public class PhysicsControl : MonoBehaviour 
{
    public static PhysicsControl control;
    PhysicsLevelState LevelState = PhysicsLevelState.loading;
    ObjectiveManager _ObjectiveManager;


    Transform Roller;
    Transform Target;

    Vector2 RollerStartPosition = Vector2.zero;

    public int AssetsInScene = 0;
    public bool BonusCollected = false;

    public float LevelTime = 10f;


    public int[] AllowedAssets = new int[5];

    void Awake()
    {
        control = this;
        Roller = GameObject.FindGameObjectWithTag("PhysicsRoller").transform;
        Target = GameObject.FindGameObjectWithTag("PhysicsTarget").transform;
        RollerStartPosition = Roller.position;

        _ObjectiveManager = this.GetComponent<ObjectiveManager>();

        UITimer.SetTimer(LevelTime);
    }
    
    void Start()
    {
        LevelState = PhysicsLevelState.building;
        DisableRoller();
    }

    void Update()
    {
        Debug.DrawRay(Roller.position, Target.position - Roller.position, Color.red);
        if (LevelState == PhysicsLevelState.playing)
        {
            float DistanceToTarget = Vector2.Distance(Target.rigidbody2D.position, Roller.rigidbody2D.position);
            
            if( DistanceToTarget < Mathf.Min( Target.collider2D.bounds.extents.x, Target.collider2D.bounds.extents.y))
            {
                CompletePhysics();
            }
        }
    }

    public void SimulatePhysics()
    {
        LevelState = PhysicsLevelState.playing;
        Lock();
        UITimer.StartTimer();
        EnableRoller();
    }

    public void ResetPhysics()
    {
        LevelState = PhysicsLevelState.building;
        Unlock();
        UITimer.ResetTimer();
        DisableRoller();
    }

    private void CompletePhysics()
    {
        LevelState = PhysicsLevelState.complete;
        UITimer.StopTimer();
        StopRoller();



        GameObject.Find("CompleteWindow").GetComponent<UIWindowComplete>().ShowComplete(
            _ObjectiveManager._Objective_1.CheckObjective(AssetsInScene),
            _ObjectiveManager._Objective_2.CheckObjective(UITimer.GetTimer()),
            _ObjectiveManager._Objective_3.CheckObjective(BonusCollected)
            );


    }

    public void ResetLevel()
    {
        //reset level stats too
        Application.LoadLevel(Application.loadedLevel);
    }

    private void EnableRoller()
    {
        Roller.rigidbody2D.isKinematic = false;
        Roller.rigidbody2D.velocity = Vector2.zero;
        Roller.rigidbody2D.position = RollerStartPosition;
        Roller.rotation = Quaternion.Euler(Vector3.zero);
    }
    private void DisableRoller()
    {
        Roller.rigidbody2D.isKinematic = true;
        Roller.rigidbody2D.velocity = Vector2.zero;
        Roller.rigidbody2D.position = RollerStartPosition;
        Roller.rotation = Quaternion.Euler(Vector3.zero);
    }
    private void StopRoller()
    {
        Roller.rigidbody2D.isKinematic = true;
        Roller.rigidbody2D.velocity = Vector2.zero;
    }

    private void Lock()
    {
        GameObject[] assets = GameObject.FindGameObjectsWithTag("PhysicsAsset");
        foreach(GameObject G in assets)
        {
            G.GetComponent<PhysicsAsset>().Lock();
        }
        GameObject.Find("AssetEditor").GetComponent<PhysicsEditor>().Lock();
    }
    private void Unlock()
    {
        GameObject[] assets = GameObject.FindGameObjectsWithTag("PhysicsAsset");
        foreach (GameObject G in assets)
        {
            G.GetComponent<PhysicsAsset>().Unlock();
        }
    }
}

public enum PhysicsLevelState
{
    loading,
    building,
    playing,
    complete,
}