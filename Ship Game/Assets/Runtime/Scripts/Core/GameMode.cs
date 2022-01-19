using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] PlayerController _playerShip;
    [SerializeField] ScreenController _screenController;
    [SerializeField] CanonBallGenerator _canonBallGenerator;

    [field: SerializeField] public int Score { get; private set; }
    private bool GameHasEnded {get; set;}

    public PlayerController GetPlayerController() => _playerShip;
    public CanonBallGenerator GetCanonBallGenerator() => _canonBallGenerator;

    public void IncreaseScore(int quant=1)
    {
        if(!GameHasEnded)
            Score += quant;
    }

    public void EndGame()
    {
        if(GameHasEnded)
            return;

        _enemySpawner.DisableAllEnemies();
        _enemySpawner.DisableSpawn();

        _playerShip.enabled = false;

        _screenController.ShowGameOverScreen();

        GameHasEnded = true;
    }

    public void RestartGame()
    {

    }
}