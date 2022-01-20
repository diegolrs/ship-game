using UnityEngine;

public class Timer : ObserverNotifier<Timer>
{
    float _maxTime;
    float _currentTime;
    bool _enableTimer;

    
    public enum TimerMode { Default, Loop }
    TimerMode _mode = TimerMode.Default;
    public float GetCurrentTime() => _currentTime;
    public float GetRemainingTime() => Mathf.Max(0, _maxTime - _currentTime);

    private void Update() 
    {
        if(_enableTimer)
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= _maxTime)
            {
                NotifyListeners();

                if(_mode == TimerMode.Loop)
                    StartTimer(_maxTime, TimerMode.Loop);
                else
                    DisabeTimer();
            }
        }
    }

    public void StartTimer(float time, TimerMode mode = TimerMode.Default)
    {
        enabled = true;

        _currentTime = 0;
        _maxTime = time;
        _enableTimer = true;

        _mode = mode;
    }

    public void DisabeTimer()
    {
        _currentTime = 0;
        _enableTimer = false;
        _mode = TimerMode.Default;
        
        enabled = false;
    }
}