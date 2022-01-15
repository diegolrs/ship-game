using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] KeyCode _forwardKey = KeyCode.UpArrow;
    [SerializeField] KeyCode _rotateLeftKey = KeyCode.LeftArrow;
    [SerializeField] KeyCode _rotateRightKey = KeyCode.RightArrow;

    public bool IsPressingForward() => _isPressingForward;
    public bool IsPressingRotateLeft() => _isPressingRotateLeft;
    public bool IsPressingRotateRight() => _isPressingRotateRight;

    private bool _isPressingForward;
    private bool _isPressingRotateLeft;
    private bool _isPressingRotateRight;

    private void Update() 
    {
        _isPressingForward = Input.GetKey(_forwardKey);
        _isPressingRotateLeft = Input.GetKey(_rotateLeftKey);
        _isPressingRotateRight = Input.GetKey(_rotateRightKey);
    }
}