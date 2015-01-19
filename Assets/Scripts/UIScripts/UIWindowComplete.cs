using UnityEngine;
using System.Collections;

public class UIWindowComplete : MonoBehaviour 
{
    public Transform Content;

    public Transform ObjectiveContainer;

    public UIObjective[] obj = new UIObjective[3];

    void Awake()
    {
        Content = transform.GetChild(0).GetChild(0);
        ObjectiveContainer = Content.GetChild(1).transform;

        obj[0] = ObjectiveContainer.GetChild(0).GetComponent<UIObjective>();
        obj[1] = ObjectiveContainer.GetChild(1).GetComponent<UIObjective>();
        obj[2] = ObjectiveContainer.GetChild(2).GetComponent<UIObjective>();
        HideComplete();
    }

    /// <summary>
    /// Change the objective states by bool array
    /// </summary>
    /// <param name="_Statuses"></param>
    public void ShowComplete(bool[] _Statuses)
    {
        obj[0].IsComplete = _Statuses[0];
        obj[1].IsComplete = _Statuses[1];
        obj[2].IsComplete = _Statuses[2];
        GetComponent<CanvasGroup>().Show();
    }
    /// <summary>
    /// Change objective states by individual bool values
    /// </summary>
    /// <param name="_Objective0"></param>
    /// <param name="_Objective1"></param>
    /// <param name="_Objective2"></param>
    public void ShowComplete(bool _Objective0, bool _Objective1, bool _Objective2)
    {

        obj[0].IsComplete = _Objective0;
        obj[1].IsComplete = _Objective1;
        obj[2].IsComplete = _Objective2;
        GetComponent<CanvasGroup>().Show();
    }
    public void HideComplete()
    {
        GetComponent<CanvasGroup>().Hide();
    }
    private void GetObjectives()
    {
        int i = 0;
        while( i < 2)
        {
            //obj[i].IsComplete = AppControl.control.GetObjective(i);
        }
    }
}
