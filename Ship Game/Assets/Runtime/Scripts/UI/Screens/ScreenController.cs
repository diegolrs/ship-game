using System;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Screen _initialScreen;
    [SerializeField] private Screen[] _screens;
    private Screen _currentScreen;

    private void Awake()
    {
        foreach (var screen in _screens)
        {
            screen.DisableScreen();
        }

        ShowScreen(_initialScreen);
    }

    private void CloseCurrentScreen() => _currentScreen?.DisableScreen();

    public void ShowMainMenuScreen() => ShowScreen(FindScreenWithType(typeof(MainMenuScreen)));
    public void ShowOptionsScreen() => ShowScreen(FindScreenWithType(typeof(OptionsScreen)));
    public void ShowGameOverScreen() => ShowScreen(FindScreenWithType(typeof(GameOverScreen)));
    public void ShowGameplayScreen() => ShowScreen(FindScreenWithType(typeof(GameplayScreen)));

    private void ShowScreen(Screen screen)
    {
        CloseCurrentScreen();
        screen?.EnableScreen();
        _currentScreen = screen;
    }

    private Screen FindScreenWithType(Type type)
    {
        foreach (var screen in _screens)
        {
            if (screen.GetComponent(type) != null)
            {
                return screen;
            }
        }
        return null;
    }
}