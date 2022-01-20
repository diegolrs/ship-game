using System.Collections;
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
        _rb.position += transform.Forward2d() * _forwardSpeed * Time.fixedDeltaTime;
    }

    public void Rotate(RotateDirection direction)
    {
        //_rb.rotation += (int)direction * _rotateSpeed * Time.fixedDeltaTime;
        var v3 = Vector3.forward * (int)direction * _rotateSpeed * Time.fixedDeltaTime;
        transform.Rotate(v3);
    }

    public void SetRotation(float rotation) => transform.SetRotation(Vector3.forward * rotation);

    public float GetInCircleDegrees(float rot)
    {
        while(rot < 0)
            rot += 360;

        while(rot > 360)
            rot -= 360;

        return rot;
    }

    public IEnumerator RotateRoutine(float target)
    {
        float from = CurrentRotation;
        target = GetInCircleDegrees(target);

        var direction = from > target ? RotateDirection.Left : RotateDirection.Right;

        if(direction == RotateDirection.Left)
        {
            while(CurrentRotation > target)
            {
                Rotate(direction);
                yield return null;
            }
        }
        else if(direction == RotateDirection.Right)
        {
            while(CurrentRotation < target)
            {
                Rotate(direction);
                yield return null;
            }
        }
    }
}