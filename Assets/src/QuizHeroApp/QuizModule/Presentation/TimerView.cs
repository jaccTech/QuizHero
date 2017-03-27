using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField] private Text countdownText;
    public string format = "Submit ({0})";

    private float timeLeft = 60.0f;
    private bool isRunning = false;
    private System.Action onDoneCallback;
    private float interval;

    void Update ()
    {
        if (!isRunning)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0f)
        {
            TimerEnded();
        }
        else
        {
            UpdateText();
        }
    }

    public float ElapsedTime { get { return interval - timeLeft; } }

    public void StartTimer(float interval, System.Action onDoneCallback)
    {
        if (isRunning)
        {
            Debug.LogWarning("Timer already running.");
            return;
        }

        this.onDoneCallback = onDoneCallback;
        this.interval = interval;
        this.timeLeft = interval;
        this.isRunning = true;
    }

    public void StopTimer ()
    {
        isRunning = false;
        onDoneCallback = null;
    }

    private void UpdateText ()
    {
        countdownText.text = string.Format(format,Mathf.RoundToInt(timeLeft));
    }

    private void TimerEnded ()
    {
        isRunning = false;

        if (onDoneCallback != null)
            onDoneCallback();
        
        onDoneCallback = null;
    }
}
