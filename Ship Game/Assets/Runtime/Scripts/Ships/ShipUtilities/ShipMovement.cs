using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _rotateSpeed;
    Rigidbody2D _rb;

    public float Rotation => transform.rotation.eulerAngles.z;

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
        _rb.position += transform.Forward2d() * _forwardSpeed * Time.fixedDeltaTime;
    }

    public void Rotate(RotateDirection direction)
    {
        //_rb.rotation += (int)direction * _rotateSpeed * Time.fixedDeltaTime;
        var v3 = Vector3.forward * (int)direction * _rotateSpeed * Time.fixedDeltaTime;
        transform.Rotate(v3);
    }

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
        float from = GetInCircleDegrees(Rotation);
        target = GetInCircleDegrees(target);

        var direction = from > target ? RotateDirection.Left : RotateDirection.Right;

        if(direction == RotateDirection.Left)
        {
            while(GetInCircleDegrees(Rotation) > target)
            {
                Rotate(direction);
                yield return null;
            }
        }
        else if(direction == RotateDirection.Right)
        {
            while(GetInCircleDegrees(Rotation) < target)
            {
                Rotate(direction);
                yield return null;
            }
        }
    }
}