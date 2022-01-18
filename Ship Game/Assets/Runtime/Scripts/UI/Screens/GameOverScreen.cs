using UnityEngine;
using TMPro;

public class GameOverScreen : Screen
{
    [SerializeField] ScreenController _screenController;
    [SerializeField] SceneController _sceneController;
    [SerializeField] SceneController.SceneName _mainMenuScene;
    [SerializeField] SceneController.SceneName _gameplayScene;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] GameMode _gameMode;

    public override void EnableScreen()
    {
        gameObject.SetActive(true);
        UpdateScore(_gameMode.Score);
    }
    
    private void UpdateScore(int score) => _text.text = $"Score: {score}";
    
    public void GoToMenu() => _sceneController?.LoadScene(_mainMenuScene);
    public void StartGame() => _sceneController?.LoadScene(_gameplayScene);
}

