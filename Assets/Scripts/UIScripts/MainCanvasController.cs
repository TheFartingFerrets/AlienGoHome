using UnityEngine;
using System.Collections;

public class MainCanvasController : MonoBehaviour 
{
    CanvasGroup WorldSelect;

    public LevelCanvasController LevelSelectController;

    void Awake()
    {
        WorldSelect = transform.GetChild(0).GetComponent<CanvasGroup>();
        LevelSelectController = GameObject.Find("Levels").GetComponent<LevelCanvasController>();
    }

    public void ShowWorldSelect()
    {
        WorldSelect.alpha = 1f;
        WorldSelect.interactable = true;
        WorldSelect.blocksRaycasts = true;

        LevelSelectController.HideAll();
    }
    
    /// <summary>
    /// Show level by level number
    /// </summary>
    /// <param name="_Level"></param>
    public void ShowLevelSelect( int _World)
    {
        WorldSelect.alpha = 0f;
        WorldSelect.interactable = false;
        WorldSelect.blocksRaycasts = false;

        LevelSelectController.ShowWorldLevels(_World);
    }
}
