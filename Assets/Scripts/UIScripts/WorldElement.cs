using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
[ExecuteInEditMode]
public class WorldElement : MonoBehaviour
{
    public string WorldName;

    void Awake()
    {
        GetComponent<Button>().transition = Selectable.Transition.None;
        GetComponent<Button>().onClick.AddListener(delegate()
        {
            AppControl.control.LoadWorld(transform.GetSiblingIndex()+1);
        });
    }


    void Update()
    {
        transform.GetChild(0).GetComponentInChildren<Text>().text = WorldName;
    }
}