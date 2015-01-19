using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    //CanvasGroup Show
    public static void Show( this CanvasGroup _CanvasGroup)
    {
        _CanvasGroup.alpha = 1f;
        _CanvasGroup.interactable = true;
        _CanvasGroup.blocksRaycasts = true;
    }
    //CanvasGroup Hide
    public static void Hide(this CanvasGroup _CanvasGroup)
    {
        _CanvasGroup.alpha = 0f;
        _CanvasGroup.interactable = false;
        _CanvasGroup.blocksRaycasts = false;
    }
}
