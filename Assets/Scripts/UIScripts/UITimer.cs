using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITimer : MonoBehaviour 
{
    //public static UITimer instance;
    [SerializeField]
    static bool running = false;
    public static bool TimesUp = false;
    public static bool Affected = false;
    public static float StartTime = 9999f;
    private static float _Time = 0;
    public static float TimeScale = 1f;

    private Text timerText;

    void Awake()
    {
        timerText = GetComponentInChildren<Text>();
        _Time = StartTime;
        timerText.text = _Time.ToString("00") + "s";
    }

    void Update()
    {
        Time.timeScale = TimeScale;

        if( running )
        {
            if( Affected )
                _Time -= 1f * Time.deltaTime;
            else
                _Time -= 1f * Time.unscaledDeltaTime;
        }

        if (running && _Time <= 0.0f)
            TimeUp();
        
        timerText.text = _Time.ToString("00")+"s";
    }

    public static void StartTimer()
    {
        running = true;
    }
    public static void StopTimer()
    {
        running = false;
    }
    public static void ResetTimer()
    {
        _Time = StartTime;
        TimesUp = false;
        running = false;
    }
    public void TimeUp()
    {
        running = false;
        TimesUp = true;
        Debug.Log("Times Up!");
    }

    public static void SetTimer(float _TimeRequirement)
    {
        Debug.Log("Setting start time");
        StartTime = _TimeRequirement;
        _Time = StartTime;
    }
    public static float GetTimer()
    {
        return _Time;
    }
}
