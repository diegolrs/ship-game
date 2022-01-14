using UnityEngine;

public class MainMenuScreen : Screen
{
    [SerializeField] ScreenController _screenController;
    [SerializeField] SceneController _sceneController;
    [SerializeField] SceneController.SceneName _gameplayScene;

    public void StartGame() => _sceneController?.LoadScene(_gameplayScene);
    public void ShowOptions() => _screenController?.ShowOptionsScreen();
}