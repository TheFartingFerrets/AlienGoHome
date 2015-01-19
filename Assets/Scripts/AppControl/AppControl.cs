using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public enum AppState
{
    Loading,
    WorldSelect,
    LevelSelect,
    InLevel,
    Options
}

public class AppControl : MonoBehaviour 
{
    public static AppControl control;
    public AppState AppState = AppState.Loading;
    [SerializeField]
    private Data AppData = new Data();

    [SerializeField]
    int World = 0;
    [SerializeField]
    int Level = 0;

    string[] worlds = {"Main", "Maths", "Physics", "Reflex", "Collect"};

    public MainCanvasController MainUIController;
    
    void Awake()
    {
        if( !control )
        {
            control = this;
            DontDestroyOnLoad(this);
        }

        //if(AppState == AppState.WorldSelect)
        MainUIController = GameObject.Find("MainUI").GetComponent<MainCanvasController>();
        
        //AppData = Data.Load(AppData);
        AppData = Data.Load();

        MainUIController.transform.GetChild(1).GetComponent<LevelCanvasController>().UpdateUI(AppData);
    }
    void Start()
    {
        //if (AppState == AppState.WorldSelect || AppState == AppState.LevelSelect)
            DeloadWorldLevel();
    }
    void Update()
    {
        if( AppState != AppState.Loading || AppState != AppState.Options)
        {
            if (Input.GetKeyDown(KeyCode.A))
                AppBack();
            if (Input.GetKeyDown(KeyCode.Escape))
                AppBack();
            if (Input.GetKeyDown(KeyCode.Menu))
                AppOptions();
        }
    }
    public void AppOptions()
    {
        Debug.Log("Toggle Options");
    }
    public void AppBack()
    {
        if( AppState == AppState.WorldSelect)
        {
            Debug.Log("App Quit");
            AppQuit();
        }
        if (AppState == AppState.InLevel)
        {
            ObjectiveManager ObjManager = GameObject.FindGameObjectWithTag("LevelController").GetComponent<ObjectiveManager>();

            UpdateObjectives(ObjManager._Objective_1.GetValue(), ObjManager._Objective_2.GetValue(), ObjManager._Objective_3.GetValue());

            StopAllCoroutines();
            StartCoroutine(LoadWorldFromLevel());
            //StartCoroutine(LoadMain());
        }
        if( AppState == AppState.LevelSelect)
        {
            DeloadWorldLevel();
        }
    }
    private void AppQuit()
    {
        Debug.Log("App Quit");
        Data.Save(AppData);
        Application.Quit();
    }
    public void LoadWorld(int _World)
    {
        AppState = AppState.LevelSelect;
        World = _World;
        MainUIController.ShowLevelSelect(_World);
    }
    public void DeloadWorldLevel()
    {
        AppState = AppState.WorldSelect;
        World = 0;
        MainUIController.ShowWorldSelect();
    }
    public void LoadWorldLevel(int _Level)
    {
        Level = _Level;
        string WorldLevel= worlds[World] + "_" + Level;
        StopAllCoroutines();
        StartCoroutine(LoadLevel(WorldLevel));
    }
    /// <summary>
    /// Load Level as Coroutine with level name
    /// </summary>
    /// <param name="_LevelName"></param>
    /// <returns></returns>
    IEnumerator LoadLevel( string _LevelName )
    {
        AppState = AppState.Loading;
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(_LevelName);
        yield return null;
        yield return null;
        AppState = AppState.InLevel;
    }
    /// <summary>
    /// Load Main
    /// </summary>
    /// <param name="_LevelNum"></param>
    /// <returns></returns>
    IEnumerator LoadMain()
    {
        AppState = AppState.Loading;
        yield return new WaitForSeconds(0.2f);
        World = 0;
        Level = 0;
        Application.LoadLevel(0);
        yield return null;
        yield return null;
        AppState = AppState.WorldSelect;
        MainUIController = GameObject.Find("MainUI").GetComponent<MainCanvasController>();
    }
    IEnumerator LoadWorldFromLevel()
    {
        Data.Save(AppData);

        yield return null;
        AppState = AppState.Loading;
        Level = 0;
        Application.LoadLevel(0);
        yield return null;
        
        MainUIController = GameObject.Find("MainUI").GetComponent<MainCanvasController>();
        MainUIController.transform.GetChild(1).GetComponent<LevelCanvasController>().UpdateUI(AppData);
        yield return null;

        AppState = AppState.LevelSelect;
        LoadWorld(World);
        
    }
    /// <summary>
    /// Unlock level by world
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Level"></param>
    public void UnlockLevel(int _World, int _Level)
    {
        GetWorld(_World).Levels[_Level].isLocked = false;
    }
    /// <summary>
    /// Unlock levels by array
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Levels"></param>
    public void UnlockLevels(int _World, int[] _Levels)
    {
        int i = 0;
        while (i < _Levels.Length)
        {
            GetWorld(_World).Levels[i].isLocked = false;
            i++;
        }
    }
    /// <summary>
    /// Lock level by world
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Level"></param>
    public void LockLevel(int _World, int _Level)
    {
        GetWorld(_World).Levels[_Level].isLocked = true;
    }
    /// <summary>
    /// Lock level by world
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Levels"></param>
    public void LockLevels(int _World, int[] _Levels)
    {
        int i = 0;
        while (i < _Levels.Length)
        {
            GetWorld(_World).Levels[i].isLocked = true;
            i++;
        }
    }

    /// <summary>
    /// Update Objectives for the current World + Level
    /// </summary>
    /// <param name="_Objectives"></param>
    public void UpdateObjectives( bool[] _Objectives)
    {
        GetWorld(World).Levels[Level].Objective_1 = _Objectives[0];
        GetWorld(World).Levels[Level].Objective_2 = _Objectives[1];
        GetWorld(World).Levels[Level].Objective_3 = _Objectives[2];
    }
    public void UpdateObjectives(bool _Objective1, bool _Objective2, bool _Objective3)
    {
        GetWorld(World).Levels[Level].Objective_1 = _Objective1;
        GetWorld(World).Levels[Level].Objective_2 = _Objective2;
        GetWorld(World).Levels[Level].Objective_3 = _Objective3;
    }
    /// <summary>
    /// Update Objectives for the selected _World + _Level
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Level"></param>
    /// <param name="_Objectives"></param>
    public void UpdateObjectives(int _World, int _Level, bool[] _Objectives)
    {
        GetWorld(_World).Levels[_Level].Objective_1 = _Objectives[0];
        GetWorld(_World).Levels[_Level].Objective_2 = _Objectives[1];
        GetWorld(_World).Levels[_Level].Objective_3 = _Objectives[2];
    }



    ///// <summary>
    ///// Update current world + level objective
    ///// </summary>
    ///// <param name="_Objective"></param>
    ///// <param name="_Status"></param>
    //public void UpdateObjective(int _Objective, bool _Status)
    //{
    //    GetWorld(World).Levels[Level].Objectives[_Objective].IsCompleted = _Status;
    //}
    ///// <summary>
    ///// Update current world + level objective
    ///// </summary>
    ///// <param name="_Objective"></param>
    ///// <param name="_Status"></param>
    //public void UpdateObjective(int[] _Objectives, bool[] _Status)
    //{
    //    int i = 0;
    //    while( i < _Objectives.Length)
    //    {
    //        GetWorld(World).Levels[Level].Objectives[i].IsCompleted = _Status[i];
    //        i++;
    //    }

    //}
    ///// <summary>
    ///// Update Level Objective
    ///// </summary>
    ///// <param name="_World"></param>
    ///// <param name="_Level"></param>
    ///// <param name="_Objective"></param>
    ///// <param name="_Status"></param>
    //public void UpdateObjective(int _World, int _Level, int _Objective, bool _Status)
    //{
    //    GetWorld(_World).Levels[_Level].Objectives[_Objective].IsCompleted = _Status;
    //}
    ///// <summary>
    ///// Update Level Objective
    ///// </summary>
    ///// <param name="_World"></param>
    ///// <param name="_Level"></param>
    ///// <param name="_Objective"></param>
    ///// <param name="_Status"></param>
    //public void UpdateObjective(int _World, int _Level, int[] _Objectives, bool[] _Status)
    //{
    //    int i = 0;
    //    while (i < _Objectives.Length)
    //    {
    //        GetWorld(_World).Levels[_Level].Objectives[i].IsCompleted = _Status[i];
    //        i++;
    //    }
    //}

    /// <summary>
    /// Return the selected World
    /// </summary>
    /// <param name="_World"></param>
    private World GetWorld(int _World)
    {
        if( _World == 1)
            return AppData.Maths;
        else if( _World == 2)
            return AppData.Physics;
        else if( _World == 3)
            return AppData.Reflex;
        else
            return AppData.Collect;
    }
    /// <summary>
    /// Get Objctive by ObjID
    /// </summary>
    /// <param name="_ObjID"></param>
    /// <returns></returns>
    //public bool GetObjective(int _ObjID)
    //{
    //    return GetWorld(World).Levels[Level].Objectives[_ObjID].IsCompleted;
    //}
    ///// <summary>
    ///// Check objectives, MinItems, MaxItems, TimeToBeat
    ///// </summary>
    ///// <param name="_MinItems"></param>
    ///// <param name="_MaxItems"></param>
    ///// <param name="_Time"></param>
    //public void CheckLevelObjectives(int _MinItems, int _MaxItems, float _Time)
    //{
    //    bool obj1 = GetWorld(World).Levels[Level].Objectives[0].CheckResult(_MinItems);
    //    bool obj2 = GetWorld(World).Levels[Level].Objectives[1].CheckResult(_MaxItems);
    //    bool obj3 = GetWorld(World).Levels[Level].Objectives[2].CheckResult(_Time);
    //}
    ///// <summary>
    ///// Check Objectives, MinItems, MaxItems, Bonus collected
    ///// </summary>
    ///// <param name="_MinItems"></param>
    ///// <param name="_MaxItems"></param>
    ///// <param name="_Bonus"></param>
    //public void CheckLevelObjectives(int _MinItems, int _MaxItems, bool _Bonus)
    //{
    //    bool obj1 = GetWorld(World).Levels[Level].Objectives[0].CheckResult(_MinItems);
    //    bool obj2 = GetWorld(World).Levels[Level].Objectives[1].CheckResult(_MaxItems);
    //    bool obj3 = GetWorld(World).Levels[Level].Objectives[2].CheckResult(_Bonus);
    //}
}
