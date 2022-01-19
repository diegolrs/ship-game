using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour, IObserver<ShipDamageable>, IObserver<Timer>
{
    [SerializeField] ShipDamageable _shipDamageable;
    [SerializeField] ShipMovement _shipMovement; 
    [SerializeField] FrontalSingleShoot _singleShoot;
    [SerializeField] SideTripleShoot _tripleShoot;
    [SerializeField] GameMode _gameMode;

    const float SingleAttackDelay = 0.45f;
    const float TripleAttackDelay = 0.35f;

    [SerializeField] Timer _attackDelayTimer;
    bool _canAttack;

    CanonBallGenerator _canonBallGenerator;
    PlayerInputs _inputs;

    public Vector2 Position => transform.position;

    public ShipDamageable GetShipDamageable() => _shipDamageable;

    private void Awake() 
    {
        _inputs = GetComponent<PlayerInputs>();
        _canonBallGenerator = _gameMode.GetCanonBallGenerator();

        _shipDamageable.AddListener(this);
        _attackDelayTimer.AddListener(this);

        _canAttack = true;
    }

    private void OnDestroy() 
    {
        _shipDamageable?.RemoveListener(this);
        _attackDelayTimer?.RemoveListener(this);
    }

    private void FixedUpdate() 
    {
        if(_inputs.IsPressingForward())
        {
            _shipMovement.MoveForward();
        }

        if(_inputs.IsPressingRotateLeft())
        {
            _shipMovement.Rotate(ShipMovement.RotateDirection.Left);
        }
        else if(_inputs.IsPressingRotateRight())
        {
            _shipMovement.Rotate(ShipMovement.RotateDirection.Right);
        }

        if(_canAttack)
        {
            if( _inputs.IsPressingSingleShoot())
            {   
                _singleShoot.Attack(transform.Forward2d(), _canonBallGenerator);
                _attackDelayTimer.StartTimer(SingleAttackDelay);
                _canAttack = false;
            }
            else if(_inputs.IsPressingTripleShoot())
            {
                _tripleShoot.Attack(transform.Forward2d(), _canonBallGenerator);
                _attackDelayTimer.StartTimer(TripleAttackDelay);
                _canAttack = false;
            }
        }
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            _gameMode.EndGame();
            enabled = false;
        }
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == _attackDelayTimer)
            _canAttack = true;
    } 
}