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
    [SerializeField]
    private AppState AppState = AppState.Loading;
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

        MainUIController = GameObject.Find("MainUI").GetComponent<MainCanvasController>();
        AppData = Data.Load();        
    }
    void Start()
    {
        DeloadWorldLevel();
    }

    void Update()
    {
        if( AppState != AppState.Loading || AppState != AppState.Options)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                AppBack();
            if (Input.GetKeyDown(KeyCode.Menu))
                AppOptions();
        }
    }
    private void AppOptions()
    {
        Debug.Log("Toggle Options");
    }
    private void AppBack()
    {
        if (AppState == AppState.InLevel)
        {
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
        AppState = AppState.Loading;
        yield return new WaitForSeconds(0.2f);
        Level = 0;
        Application.LoadLevel(0);
        yield return null;
        yield return null;
        MainUIController = GameObject.Find("MainUI").GetComponent<MainCanvasController>();
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
    /// Lock level by world
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Level"></param>
    public void LockLevel(int _World, int _Level)
    {
        GetWorld(_World).Levels[_Level].isLocked = true;
    }
    /// <summary>
    /// Update Level Objective
    /// </summary>
    /// <param name="_World"></param>
    /// <param name="_Level"></param>
    /// <param name="_Objective"></param>
    /// <param name="_Status"></param>
    public void UpdateObjective(int _World, int _Level, int _Objective, bool _Status)
    {
        GetWorld(_World).Levels[_Level].Objectives[_Objective].IsCompleted = _Status;
    }
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
}
