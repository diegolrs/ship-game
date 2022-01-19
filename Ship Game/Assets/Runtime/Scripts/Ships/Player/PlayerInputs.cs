using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] KeyCode _forwardKey = KeyCode.UpArrow;
    [SerializeField] KeyCode _rotateLeftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode _rotateRightKey = KeyCode.RightArrow;
    [SerializeField] KeyCode _singleShoot = KeyCode.J;
    [SerializeField] KeyCode _tripleShoot = KeyCode.K;

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
        _isPressingForward = Input.GetKey(_forwardKey);
        _isPressingRotateLeft = Input.GetKey(_rotateLeftKey);
        _isPressingRotateRight = Input.GetKey(_rotateRightKey);
        _isPressingSingleShoot = Input.GetKey(_singleShoot);
        _isPressingTripleShoot = Input.GetKey(_tripleShoot);
    }
}