using UnityEngine;

public class ShooterController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [Header("Attack Parameters")]
    [SerializeField] ShipDamageable _shooterShipDamage;
    [SerializeField] float _minDistanceToShoot = 1.5f;
    [SerializeField] FrontalSingleShoot _attack;

    CanonBallGenerator _canonBallGenerator;
    PlayerController _player;
    bool _shouldShoot;


    #region Enemy Ship Interface
    public int PointsToAddAfterBeKilled => 1;
    public float DelayToDisableAfterBeKilled => 2f;
    public EnemySpawner EnemySpawner { get; set; }
    public GameMode GameMode {get; private set;}
    [field: SerializeField] public Timer DisableAfterBeKilledTimer { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        this.EnemySpawner = spawner;
        this.GameMode = gameMode;

        this._player = gameMode.GetPlayerController();
        this._canonBallGenerator = gameMode.GetCanonBallGenerator();
        
        DisableAfterBeKilledTimer.DisabeTimer();
    }
    #endregion

    #region Gizmos
    [Header("Gizmos")]
    [SerializeField] Color _gizmosColorWhenShouldShoot = Color.red;
    [SerializeField] Color _gizmosColorWhenShouldNotShoot = Color.green;
    

    private void OnDrawGizmos() 
    {
        Gizmos.color = _shouldShoot ? _gizmosColorWhenShouldShoot : _gizmosColorWhenShouldNotShoot;
        Gizmos.DrawWireSphere(transform.position, _minDistanceToShoot);
    }
    #endregion

    private void Awake()
    {
        DisableAfterBeKilledTimer.AddListener(this);
        _shooterShipDamage.AddListener(this);
    }

    private void Update() 
    {
        ProcessAttack();
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsToAddAfterBeKilled);
            DisableAfterBeKilledTimer.StartTimer(DelayToDisableAfterBeKilled);
        }
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == DisableAfterBeKilledTimer)
            EnemySpawner.UnspawnEnemy(gameObject);
    }

    private void ProcessAttack()
    {
        _shouldShoot =  _player != null
                        && _canonBallGenerator != null
                        && !_shooterShipDamage.IsDead() 
                        && !_attack.IsWaitingCoolDownEnds
                        && Vector2.Distance(transform.position, _player.Position) <= _minDistanceToShoot;

        if(_shouldShoot)
        {
            var shootDirection = (_player.transform.position - transform.position).normalized;   
            _attack.Attack(shootDirection, _canonBallGenerator);
        }
    }
}