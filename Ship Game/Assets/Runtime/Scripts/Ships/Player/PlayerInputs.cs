using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [field: SerializeField] public KeyCode ForwardKey { get; private set; } = KeyCode.UpArrow;
    [field: SerializeField] public KeyCode RotateLeftKey { get; private set; } = KeyCode.LeftArrow;
    [field: SerializeField] public KeyCode RotateRightKey { get; private set; } = KeyCode.RightArrow;
    [field: SerializeField] public KeyCode FrontalShoot { get; private set; } = KeyCode.Z;
    [field: SerializeField] public KeyCode SideShoot { get; private set; } = KeyCode.X;

    public bool IsPressingForward() => _isPressingForward;
    public bool IsPressingRotateLeft() => _isPressingRotateLeft;
    public bool IsPressingRotateRight() => _isPressingRotateRight;
    public bool IsPressingFrontalShoot() => _isPressingFrontalShoot;
    public bool IsPressingSideShoot() => _isPressingSideShoot;

    private bool _isPressingForward;
    private bool _isPressingRotateLeft;
    private bool _isPressingRotateRight;
    private bool _isPressingFrontalShoot;
    private bool _isPressingSideShoot;

    private void Update() 
    {
        _isPressingForward = Input.GetKey(ForwardKey);
        _isPressingRotateLeft = Input.GetKey(RotateLeftKey);
        _isPressingRotateRight = Input.GetKey(RotateRightKey);
        _isPressingFrontalShoot = Input.GetKey(FrontalShoot);
        _isPressingSideShoot = Input.GetKey(SideShoot);
    }
}