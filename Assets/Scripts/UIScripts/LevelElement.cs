using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
[ExecuteInEditMode]
public class LevelElement : MonoBehaviour 
{
    [SerializeField]
    UIObjective[] ObjectivesElement = new UIObjective[3];
    void Awake()
    {
        transform.GetComponentInChildren<Text>().text = (transform.GetSiblingIndex() + 1).ToString();

        ObjectivesElement[0] = transform.GetChild(1).GetChild(0).GetComponent<UIObjective>();
        ObjectivesElement[1] = transform.GetChild(1).GetChild(1).GetComponent<UIObjective>();
        ObjectivesElement[2] = transform.GetChild(1).GetChild(2).GetComponent<UIObjective>();


        GetComponent<Button>().onClick.AddListener(delegate()
        {
            AppControl.control.LoadWorldLevel(transform.GetSiblingIndex());
        });
    }

    void SetObjectives( bool _Objective1, bool _Objective2, bool _Objective3)
    {
        ObjectivesElement[0].IsComplete = _Objective1;
        ObjectivesElement[1].IsComplete = _Objective2;
        ObjectivesElement[2].IsComplete = _Objective3;
    }


    ///// <summary>
    ///// Display objectives being true or false
    ///// </summary>
    ///// <param name="_Objective1"></param>
    ///// <param name="_Objective2"></param>
    ///// <param name="_Objective3"></param>
    //void SetObjectives(bool _Objective1, bool _Objective2, bool _Objective3)
    //{
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objective1;
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objective2;
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objective3;
    //}
    ///// <summary>
    ///// Display objectives being true or false by Objective Array
    ///// </summary>
    ///// <param name="_Objectives"></param>
    //void SetObjectives(Objective[] _Objectives)
    //{
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objectives[0].IsCompleted;
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objectives[0].IsCompleted;
    //    transform.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = _Objectives[0].IsCompleted;
    //}

}
