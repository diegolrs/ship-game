using UnityEngine;

public class GameSession : MonoBehaviour, IObserver<Timer>
{
    [SerializeField] GameMode _gameMode;
    [SerializeField] GameplaySettingsSO _gameplaySettingsSO;
    [SerializeField] Timer _gameSessionTimer;

    private void Awake() => _gameSessionTimer?.AddListener(this);
    private void OnDestroy() => _gameSessionTimer?.RemoveListener(this);
    private void Start()  => _gameSessionTimer.StartTimer(_gameplaySettingsSO.GameSessionTime);    
    
    public void OnNotified(Timer notifier)
    {
        _gameMode.EndGame();
    }
}