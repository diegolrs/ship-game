using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsScreen : Screen
{
    [SerializeField] GameplaySettingsSO _settingsSO;
    [SerializeField] ScreenController _screenController;
    [SerializeField] Slider _gameSessionTimeSlider;
    [SerializeField] Slider _enemySpawnTimeSlider;

    [Header("Hud Texts")]
    [SerializeField, Tooltip("Text that shows the current value")] TextMeshProUGUI _gameSessionValueHud;
    [SerializeField, Tooltip("Text that shows the current value")] TextMeshProUGUI _enemySpawnValueHud;
    private string TimeToHudString(float time) =>  time.ToString("0.##") + "s";

    private void Awake() 
    {
        _gameSessionTimeSlider.onValueChanged.AddListener(UpdateGameSessionTime);
        _enemySpawnTimeSlider.onValueChanged.AddListener(UpdateEnemySpawnTime);

        _gameSessionTimeSlider.minValue = _settingsSO.MinGameSessionTime;
        _gameSessionTimeSlider.maxValue = _settingsSO.MaxGameSessionTime;
        _enemySpawnTimeSlider.minValue = _settingsSO.MinEnemySpawnTime;
        _enemySpawnTimeSlider.maxValue = _settingsSO.MaxEnemySpawnTime;

        SyncSlidersWithSettings();
    }

    private void OnDestroy()
    {
        _gameSessionTimeSlider?.onValueChanged.RemoveListener(UpdateGameSessionTime);
        _enemySpawnTimeSlider?.onValueChanged.RemoveListener(UpdateEnemySpawnTime);
    }

    private void SyncSlidersWithSettings()
    {
        _gameSessionTimeSlider.value = _settingsSO.GameSessionTime;
        _enemySpawnTimeSlider.value = _settingsSO.EnemySpawnTime;
    }

    public void BackToMainMenuScreen() => _screenController?.ShowMainMenuScreen();

    public void UpdateGameSessionTime(float newTime)
    {
        _settingsSO.SetGameSessionTime(newTime);
        _gameSessionValueHud.text = TimeToHudString(newTime);
    }
    public void UpdateEnemySpawnTime(float newTime)
    {   
        _settingsSO.SetEnemySpawnTime(newTime);
        _enemySpawnValueHud.text = TimeToHudString(newTime);
    }
}