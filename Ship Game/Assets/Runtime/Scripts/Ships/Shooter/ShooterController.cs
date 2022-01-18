using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ShooterController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipDamageable _shooterShipDamage;
    [SerializeField] float _minDistanceToShoot = 1.5f;
    bool shouldShoot;

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

        UnspawnTimer = GetComponent<Timer>();
        UnspawnTimer.AddListener(this);
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

    private void Update()
    {
        if(!WasSetuped)
            return;

        shouldShoot = Vector2.Distance(transform.position, _player.Position) < _minDistanceToShoot;
        if(shouldShoot) ShootPlayer();
    }

    private void ShootPlayer()
    {
        Debug.Log("player should be shooted");
    }

    public void OnNotified(Timer notifier)
    {
        EnemySpawner.UnspawnEnemy(gameObject);
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.GetCurrentStatus() == ShipDamageable.DamageStatus.Dead)
        {
            GameMode.IncreaseScore(PointsPerDeath);
            UnspawnTimer.StartTimer(UnspawnTime);
        }
    }
}