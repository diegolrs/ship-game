using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] PlayerController _playerShip;

    public PlayerController GetPlayerShip() => _playerShip;

    public void EndGame()
    {

    }

    public void RestartGame()
    {

    }
}