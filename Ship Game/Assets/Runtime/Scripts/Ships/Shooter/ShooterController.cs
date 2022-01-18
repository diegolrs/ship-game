using UnityEngine;

[RequireComponent(typeof(Timer))]
[RequireComponent(typeof(ICanonBallAttack))]
public class ShooterController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipDamageable _shooterShipDamage;
    [SerializeField] float _minDistanceToShoot = 1.5f;
    [SerializeField] float _attackDelay = 0.8f;
    bool shouldShoot;
    ICanonBallAttack _attack;
    private Timer _attackTimer;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float UnspawnTime => 2f;
    public EnemySpawner EnemySpawner { get; set; }
    public GameMode GameMode {get; private set;}
    public Timer UnspawnTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        this.EnemySpawner = spawner;
        this.GameMode = gameMode;
        this._player = gameMode.GetPlayerShip();

        if(UnspawnTimer == null)
        {
            UnspawnTimer = GetComponent<Timer>();
            UnspawnTimer.AddListener(this);
        }

        UnspawnTimer.DisabeTimer();
        
        _shooterShipDamage.AddListener(this);

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
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == UnspawnTimer)
            EnemySpawner.UnspawnEnemy(gameObject);
        else if(notifier == _attackTimer)
            ProcessAttack();
    }

    private void ProcessAttack()
    {
        bool shouldShoot =  !_shooterShipDamage.IsDead() 
                            && Vector2.Distance(transform.position, _player.Position) < _minDistanceToShoot;

        if(shouldShoot)
            _attack.Attack();
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsPerDeath);
            UnspawnTimer.StartTimer(UnspawnTime);
        }
    }
}