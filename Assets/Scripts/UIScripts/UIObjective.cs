using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class UIObjective : MonoBehaviour 
{
    public bool IsComplete = false;

    public Sprite NotCompleteImage;
    public Sprite CompleteImage;

    void Awake()
    {
        GetComponent<Image>().sprite = NotCompleteImage;
        GetComponent<Image>().preserveAspect = true;
    }

    public void CompleteObjective()
    {
        IsComplete = true;
    }

    void Update()
    {
        if( !IsComplete )
        {
            if (NotCompleteImage != null)
                GetComponent<Image>().sprite = NotCompleteImage;
        }
        if( IsComplete)
        {
            if (CompleteImage != null)
                GetComponent<Image>().sprite = CompleteImage;
        }
    }


}
