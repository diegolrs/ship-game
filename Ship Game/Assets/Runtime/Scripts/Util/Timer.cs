using UnityEngine;

public class Timer : ObserverNotifier<Timer>
{
    float _maxTime;
    float _currentTime;
    bool _enableTimer;
    bool _shouldLoop;

    private void Update() 
    {
        if(_enableTimer)
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= _maxTime)
            {
                NotifyListeners();

                if(_shouldLoop)
                    StartTimer(_maxTime, _shouldLoop);
                else
                    DisabeTimer();
            }
        }
    }

    public void StartTimer(float time, bool shouldLoop=false)
    {
        enabled = true;

        _currentTime = 0;
        _maxTime = time;
        _enableTimer = true;

        _shouldLoop = shouldLoop;
    }

    public void DisabeTimer()
    {
        _currentTime = 0;
        _enableTimer = false;
        _shouldLoop = false;
        
        enabled = false;
    }
}