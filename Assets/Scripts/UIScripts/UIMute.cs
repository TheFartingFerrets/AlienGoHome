using UnityEngine;
using System.Collections;

public class UIMute : MonoBehaviour 
{
    public enum AudioTarget
    {
        None,
        Background,
        SoundEffects,
        Needmore,
    }
    public bool muted = false;
    
    public AudioTarget Target = AudioTarget.None;

    void Awake()
    {
        AudioListener.pause = muted;
    }
    /// <summary>
    /// Toggles audio playback
    /// </summary>
    public void ToggleAudio()
    {
        //Mutes all audio
        //Look into muting background music
        //and muting sound effects
        muted = !muted;
        AudioListener.pause = muted;
    }
	
}
