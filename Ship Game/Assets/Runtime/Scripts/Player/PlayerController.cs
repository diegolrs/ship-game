using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] ShipMovement _playerShip;
    PlayerInputs _inputs;

    private void Awake() 
    {
        _inputs = GetComponent<PlayerInputs>();
    }

    private void FixedUpdate() 
    {
        if(_inputs.IsPressingForward())
        {
            _playerShip.MoveForward();
        }

        if(_inputs.IsPressingRotateLeft())
        {
            _playerShip.Rotate(ShipMovement.RotateDirection.Left);
        }
        else if(_inputs.IsPressingRotateRight())
        {
            _playerShip.Rotate(ShipMovement.RotateDirection.Right);
        }
    }
}