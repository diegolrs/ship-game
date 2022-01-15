using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] ShipMovement _shipMovement;
    PlayerInputs _inputs;

    private void Awake() 
    {
        _inputs = GetComponent<PlayerInputs>();
    }

    private void FixedUpdate() 
    {
        if(_inputs.IsPressingForward())
        {
            _shipMovement.MoveForward();
        }

        if(_inputs.IsPressingRotateLeft())
        {
            _shipMovement.Rotate(ShipMovement.RotateDirection.Left);
        }
        else if(_inputs.IsPressingRotateRight())
        {
            _shipMovement.Rotate(ShipMovement.RotateDirection.Right);
        }
    }
}