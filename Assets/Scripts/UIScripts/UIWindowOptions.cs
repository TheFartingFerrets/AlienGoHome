using UnityEngine;
using System.Collections;

public class UIWindowOptions: MonoBehaviour 
{
    bool isOpen = false;
    public Transform Content;

    void Awake()
    {
        Hide();
    }
    public void ToggleOptions()
    {
        if( isOpen )
        {
            Hide();
        }
        else
        {
            Show();
        }
        isOpen = !isOpen;
    }

    private void Show()
    {
        GetComponent<CanvasGroup>().Show();
    }
    private void Hide()
    {
        GetComponent<CanvasGroup>().Hide();
    }
}
