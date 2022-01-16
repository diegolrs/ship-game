using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _rotateSpeed;
    Rigidbody2D _rb;

    public enum RotateDirection
    {
        Left = 1,
        Right = -1
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void MoveForward()
    {
        Debug.Log(transform.Forward2d());

        _rb.position += transform.Forward2d() * _forwardSpeed * Time.fixedDeltaTime;
    }

    public void Rotate(RotateDirection direction)
    {
        _rb.rotation += (int)direction * _rotateSpeed * Time.fixedDeltaTime;
    }
}