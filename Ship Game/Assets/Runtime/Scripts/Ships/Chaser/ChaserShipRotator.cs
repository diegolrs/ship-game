using UnityEngine;

public class ChaserShipRotator : MonoBehaviour
{
    [SerializeField] ShipMovement _chaserShip;

    private enum Rotations
    {
        LeftAngle = 180,
        RightAngle = 0,
        TopAngle = 90,
        BottomAngle = 270,

        LeftTopAngle = 135,
        LeftBottomAngle = 225,
        RightTopAngle = 45,
        RightBottomAngle = 315,

        NoDirection = 0
    }

    float _curRotateTime = 0;
    float _maxRotateTime = 1.5f;
    float _initialRotation = 0;
    float _targetRotation = 0;
    bool _isRotating = false;

    public bool IsRotating() => _isRotating;
    private void OnEnable() => StopRotation();

    private void Update() 
    {
        if(!_isRotating)
            return;

        _curRotateTime += Time.deltaTime;

        float clampedRotation = MathUtils.LerpClamped(_initialRotation, _targetRotation, _curRotateTime);
        _chaserShip.SetRotation(clampedRotation);

        _isRotating = _curRotateTime < _maxRotateTime;
    }

    public void StartRotation(Vector2 dir)
    {
        StopRotation();

        _initialRotation =  _chaserShip.transform.GetZRotationInDegrees();
        _targetRotation = (float) GetRotationToDirection(dir);

        _isRotating = true;
    }

    private void StopRotation()
    {
        _isRotating = false;
        _curRotateTime = 0;
    }

    Rotations GetRotationToDirection(Vector2 dir)
    {
        float minDeltaX = 0.12f;
        float minDeltaY = 0.12f;
        if(Mathf.Abs(dir.x) <= minDeltaX) dir.x = 0;
        if(Mathf.Abs(dir.y) <= minDeltaY) dir.y = 0;

        bool goToRight = dir.x > 0;
        bool goToLeft = dir.x < 0;
        bool goUp = dir.y > 0;
        bool goDown = dir.y < 0;
        
        if(goUp && goToRight) return Rotations.RightTopAngle;
        if(goUp && goToLeft) return Rotations.LeftTopAngle;
        if(goDown && goToRight) return Rotations.RightBottomAngle;
        if(goDown && goToLeft) return Rotations.LeftBottomAngle;

        if(goUp) return Rotations.TopAngle;
        if(goDown) return Rotations.BottomAngle;
        if(goToLeft) return Rotations.LeftAngle;
        if(goToRight) return Rotations.RightAngle;

        return Rotations.NoDirection;
    }
}