using UnityEngine;

public class ChaserController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipMovement _chaserShipMovement;
    [SerializeField] ShipDamageable _chaserShipDamage;
    [SerializeField] ChaserShipRotator _chaserShipRotator;
    [SerializeField] int _explosionDamageValue = 30;


    #region Enemy Ship Interface
    public int PointsToAddAfterBeKilled => 1;
    public float DelayToDisableAfterBeKilled => 2f;
    public EnemySpawner EnemySpawner { get; private set; }
    public GameMode GameMode {get; private set;}
    [field: SerializeField] public Timer DisableAfterBeKilledTimer { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        _chaserShipDamage.AddListener(this);

        this.EnemySpawner = spawner;
        this.GameMode = gameMode;
        _player = gameMode.GetPlayerController();
        DisableAfterBeKilledTimer.DisabeTimer();
    }
    #endregion

    private void Awake()
    {
        DisableAfterBeKilledTimer.AddListener(this);
        _chaserShipDamage.AddListener(this);
    }

    private void OnDestroy() 
    {
        DisableAfterBeKilledTimer?.RemoveListener(this);
        _chaserShipDamage?.RemoveListener(this);
    }

    private void Update() 
    {
        if(!_chaserShipDamage.IsDead())
            ChasePlayer();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<PlayerController>() is PlayerController player)
            Explode(player);
    }

    private void ChasePlayer()
    {
        if(_player == null)
            return;

        _chaserShipMovement.MoveForward();

        if(!_chaserShipRotator.IsRotating())
        {
            Vector2 dir = new Vector2()
            {
                x = _chaserShipMovement.Position.x > _player.Position.x ? -1 : 1,            
                y = _chaserShipMovement.Position.y > _player.Position.y ? -1 : 1
            };

            _chaserShipRotator.StartRotation(dir.normalized);
        }
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
    
    private void Explode(PlayerController playerToExplode)
    {
        if(!_chaserShipDamage.IsDead())
        {
            playerToExplode?.GetShipDamageable().TakeDamage(_explosionDamageValue);
            _chaserShipDamage.TakeDamage(_chaserShipDamage.GetMaxHealthy());
        }
    }
}