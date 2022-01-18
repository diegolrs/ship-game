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

    public IEnumerator RotateRoutine(float curAngle, float targetAngle, float time)
    {
        float rot = Rotation;

        while(rot < 0)
            rot += 360;

        while(rot > 0)
            rot -= 360;

        float delta = targetAngle-curAngle;
        float step = delta/time;

        var direction = delta > 0 ? RotateDirection.Right : RotateDirection.Left;

        curAngle = Rotation;

        while(Mathf.Abs(curAngle/targetAngle) < 1)
        {
            break;

            curAngle = Rotation;
            curAngle += step; 
            
            var v3 = Vector3.forward * (int)direction * _rotateSpeed * Time.fixedDeltaTime;
            //transform.rotation += new Quaternion(v3.x, v3.y, v3.z, 0);

            Rotate(direction);
            yield return new WaitForSeconds(Mathf.Abs(step));
            
        }
    }
}