using UnityEngine;
using System.Collections;

public class LevelCanvasController : MonoBehaviour
{

    Transform Maths;
    Transform Physics;
    Transform Reflex;
    Transform Collect;

    void Awake()
    {
        HideAll();
        Maths = transform.GetChild(0);
        Physics = transform.GetChild(1);
        Reflex = transform.GetChild(2);
        Collect = transform.GetChild(3);
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

    public void UpdateUI(Data _Data)
    {

        UpdateWorldLevels(0, _Data.Maths);
        UpdateWorldLevels(1, _Data.Physics);
        UpdateWorldLevels(2, _Data.Reflex);
        UpdateWorldLevels(3, _Data.Collect);
    }

    private void UpdateWorldLevels(int WorldID, World _World)
    {
        for (int i = 0; i < 9; i++)
        {
            this.transform.GetChild(WorldID).GetChild(1).GetChild(i).GetComponent<LevelElement>().SetObjectives(
                _World.Levels[i].Objective_1,
                _World.Levels[i].Objective_2,
                _World.Levels[i].Objective_3
                );
        }
    }
}
