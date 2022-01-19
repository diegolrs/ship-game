using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [field: SerializeField] public KeyCode ForwardKey { get; private set; } = KeyCode.UpArrow;
    [field: SerializeField] public KeyCode RotateLeftKey { get; private set; } = KeyCode.LeftArrow;
    [field: SerializeField] public KeyCode RotateRightKey { get; private set; } = KeyCode.RightArrow;
    [field: SerializeField] public KeyCode SingleShoot { get; private set; } = KeyCode.Z;
    [field: SerializeField] public KeyCode TripleShoot { get; private set; } = KeyCode.X;

    public bool IsPressingForward() => _isPressingForward;
    public bool IsPressingRotateLeft() => _isPressingRotateLeft;
    public bool IsPressingRotateRight() => _isPressingRotateRight;
    public bool IsPressingSingleShoot() => _isPressingSingleShoot;
    public bool IsPressingTripleShoot() => _isPressingTripleShoot;

    private bool _isPressingForward;
    private bool _isPressingRotateLeft;
    private bool _isPressingRotateRight;
    private bool _isPressingSingleShoot;
    private bool _isPressingTripleShoot;

    private void Update() 
    {
        _isPressingForward = Input.GetKey(ForwardKey);
        _isPressingRotateLeft = Input.GetKey(RotateLeftKey);
        _isPressingRotateRight = Input.GetKey(RotateRightKey);
        _isPressingSingleShoot = Input.GetKey(SingleShoot);
        _isPressingTripleShoot = Input.GetKey(TripleShoot);
    }
}