using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlayPause : MonoBehaviour 
{
    bool IsPlaying = false;

    public Sprite PlaySprite;
    public Sprite PauseSprite;

    void Awake()
    {
        GetComponent<Image>().sprite = PlaySprite;
    }

    public void SwitchGameState()
    {
        IsPlaying = !IsPlaying;

        if (IsPlaying)
        {
            GameObject.Find("PhysicsControl").SendMessage("SimulatePhysics");
            GetComponent<Image>().sprite = PauseSprite;
        }
        else
        {
            GameObject.Find("PhysicsControl").SendMessage("ResetPhysics");
            GetComponent<Image>().sprite = PlaySprite;
        }
    }
}
