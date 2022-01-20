using UnityEngine;

[RequireComponent(typeof(ICanonBallAttack))]
public class ShooterController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipDamageable _shooterShipDamage;
    [SerializeField] float _minDistanceToShoot = 1.5f;
    [SerializeField] float _attackDelay = 0.8f;
    bool _shouldShoot;
    ICanonBallAttack _attack;
    CanonBallGenerator _canonBallGenerator;

    [SerializeField] private Timer _attackTimer;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float TimeToDisableAfterDeath => 2f;
    public EnemySpawner EnemySpawner { get; set; }
    public GameMode GameMode {get; private set;}
    [field: SerializeField] public Timer DisableTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        this.EnemySpawner = spawner;
        this.GameMode = gameMode;

        this._player = gameMode.GetPlayerController();
        this._canonBallGenerator = gameMode.GetCanonBallGenerator();
        
        DisableTimer.DisabeTimer();
        _attackTimer.StartTimer(_attackDelay, Timer.TimerMode.Loop);

        WasSetuped = true;
    }
    #endregion

    #region Gizmos
    [Header("Gizmos")]
    [Tooltip("Color to apply on Gizmos when player is out of the attack radius")]
    [SerializeField] Color _colorWhenCantShoot = Color.green;
    [Tooltip("Color to apply on Gizmos when player is inside the attack radius")]
    [SerializeField] Color _colorWhenCanShoot = Color.red;

    private void OnDrawGizmos() 
    {
        Gizmos.color = _shouldShoot ? _colorWhenCanShoot : _colorWhenCantShoot;
        Gizmos.DrawWireSphere(transform.position, _minDistanceToShoot);
    }
    #endregion

    private void Awake()
    {
        _attack = GetComponent<ICanonBallAttack>(); 
        DisableTimer.AddListener(this);
        _attackTimer.AddListener(this);
        _shooterShipDamage.AddListener(this);
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == DisableTimer)
            EnemySpawner.UnspawnEnemy(gameObject);
        else if(notifier == _attackTimer)
            ProcessAttack();
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsPerDeath);
            DisableTimer.StartTimer(TimeToDisableAfterDeath);
            _attackTimer.DisabeTimer();
        }
    }

    private void ProcessAttack()
    {
        _shouldShoot =  WasSetuped
                        && !_shooterShipDamage.IsDead() 
                        && Vector2.Distance(transform.position, _player.Position) <= _minDistanceToShoot;

        if(_shouldShoot)
        {
            var playerShip = _player.transform.position;
            var thisShip = transform.position;

            float x = playerShip.x - thisShip.x;
            float y = playerShip.y - thisShip.y;

            var dir = new Vector2(x, y).normalized;
            
            _attack.Attack(dir, _canonBallGenerator);
        }
    }
}