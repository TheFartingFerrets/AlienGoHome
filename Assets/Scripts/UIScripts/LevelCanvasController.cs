using UnityEngine;
using System.Collections;

public class LevelCanvasController : MonoBehaviour
{
    void Awake()
    {
        HideAll();
    }
    /// <summary>
    /// Shows the world level selections
    /// </summary>
    /// <param name="_World"></param>
    public void ShowWorldLevels(int _World)
    {
        HideAll();

        _World -= 1;

        transform.GetChild(_World).GetComponent<CanvasGroup>().Show();
    }

    public void HideAll()
    {
        foreach (Transform cg in transform)
        {
            cg.GetComponent<CanvasGroup>().Hide();
        }
    }
}
