using UnityEngine;

[RequireComponent(typeof(ICanonBallAttack))]
public class ShooterController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipDamageable _shooterShipDamage;
    [SerializeField] float _minDistanceToShoot = 1.5f;
    [SerializeField] float _attackDelay = 0.8f;
    bool shouldShoot;
    ICanonBallAttack _attack;
    CanonBallGenerator _canonBallGenerator;

    [SerializeField] private Timer _attackTimer;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float UnspawnTime => 2f;
    public EnemySpawner EnemySpawner { get; set; }
    public GameMode GameMode {get; private set;}
    [field: SerializeField] public Timer UnspawnTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        this.EnemySpawner = spawner;
        this.GameMode = gameMode;

        this._player = gameMode.GetPlayerShip();
        this._canonBallGenerator = gameMode.GetCanonBallGenerator();
        
        UnspawnTimer.DisabeTimer();
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
        Gizmos.color = shouldShoot ? _colorWhenCanShoot : _colorWhenCantShoot;
        Gizmos.DrawWireSphere(transform.position, _minDistanceToShoot);
    }
    #endregion

    private void Awake()
    {
        _attack = GetComponent<ICanonBallAttack>(); 
        UnspawnTimer.AddListener(this);
        _attackTimer.AddListener(this);
        _shooterShipDamage.AddListener(this);
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == UnspawnTimer)
            EnemySpawner.UnspawnEnemy(gameObject);
        else if(notifier == _attackTimer)
            ProcessAttack();
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsPerDeath);
            UnspawnTimer.StartTimer(UnspawnTime);
            _attackTimer.DisabeTimer();
        }
    }

    private void ProcessAttack()
    {
        bool shouldShoot =  WasSetuped
                            && !_shooterShipDamage.IsDead() 
                            && Vector2.Distance(transform.position, _player.Position) <= _minDistanceToShoot;

        if(shouldShoot)
        {
            var curr = transform.position;
            var target = _player.Position;
            var dir = Vector2.one;

            dir.x = curr.x >= target.x ? -1 : 1;
            dir.y = curr.y >= target.y ? -1 : 1;

            _attack.Attack(dir, _canonBallGenerator);
        }
    }
}