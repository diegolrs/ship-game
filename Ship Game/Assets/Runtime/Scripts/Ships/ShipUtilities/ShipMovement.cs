using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _rotateSpeed;
    Rigidbody2D _rb;

    public Vector3 Position => transform.position;
    public float CurrentRotation => transform.GetZRotationInDegrees();

    public enum RotateDirection { Left = 1, Right = -1 }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    public void MoveForward()
    {
        if(_rb != null)
            _rb.position += transform.Forward2d() * _forwardSpeed * Time.fixedDeltaTime;
    }

    public void Rotate(RotateDirection direction)
    {
        var v3 = Vector3.forward * (int)direction * _rotateSpeed * Time.fixedDeltaTime;
        transform.Rotate(v3);
    }

    public void SetRotation(float rotation) => transform.SetRotation(Vector3.forward * rotation);
}