using UnityEngine;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerController : MonoBehaviour, IObserver<ShipDamageable>
{
    [SerializeField] ShipDamageable _shipDamageable;
    [SerializeField] ShipMovement _shipMovement; 
    [SerializeField] FrontalSingleShoot _frontalShoot;
    [SerializeField] SideTripleShoot _sideShoot;
    [SerializeField] GameMode _gameMode;

    private PlayerInputs _inputs;
    private CanonBallGenerator _canonBallGenerator;

    public Vector2 Position => transform.position;
    public ShipDamageable GetShipDamageable() => _shipDamageable;
    public PlayerInputs GetPlayerInputs() => _inputs;
    public float GetFrontalShootCoolDown() => _frontalShoot.CoolDownTimer.GetRemainingTime();
    public float GetSideShootCoolDown() => _sideShoot.CoolDownTimer.GetRemainingTime();

    private void Awake() 
    {
        _gameMode ??= FindObjectOfType<GameMode>();

        _inputs = GetComponent<PlayerInputs>();
        _canonBallGenerator = _gameMode.GetCanonBallGenerator();

        _shipDamageable.AddListener(this);
    }

    private void OnDestroy() 
    {
        _shipDamageable?.RemoveListener(this);
    }

    private void FixedUpdate()
    {
        ProcessInputs();
        ProcessAttacks();
    }

    private void ProcessInputs()
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
    }

    private void ProcessAttacks()
    {
        if (_inputs.IsPressingFrontalShoot() && !_frontalShoot.IsWaitingCoolDownEnds)
            _frontalShoot.Attack(transform.Forward2d(), _canonBallGenerator);
        else if (_inputs.IsPressingSideShoot() && !_sideShoot.IsWaitingCoolDownEnds)
            _sideShoot.Attack(transform.Forward2d(), _canonBallGenerator);
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            _gameMode.EndGame();
            enabled = false;
        }
    }
}