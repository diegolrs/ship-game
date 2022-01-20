using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ChaserController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipMovement _chaserShipMovement;
    [SerializeField] ShipDamageable _chaserShipDamage;
    [SerializeField] ChaserShipRotator _chaserShipRotator;

    private const int DamageValue = 30;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float TimeToDisableAfterDeath => 2f;
    public EnemySpawner EnemySpawner { get; private set; }
    public GameMode GameMode {get; private set;}
    public Timer DisableTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        if(DisableTimer == null)
        {
            DisableTimer = GetComponent<Timer>();
            DisableTimer.AddListener(this);
        }

        DisableTimer.DisabeTimer();


        _chaserShipDamage.AddListener(this);

        this.EnemySpawner = spawner;
        this.GameMode = gameMode;
        _player = gameMode.GetPlayerController();

        WasSetuped = true;
    }
    #endregion

    private void OnDisable()
    {
        WasSetuped = false;
    }

    private void OnDestroy() 
    {
        DisableTimer?.RemoveListener(this);
        _chaserShipDamage?.RemoveListener(this);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<PlayerController>() != null)
            Explode();
    }

    public void OnNotified(Timer notifier)
    {
        EnemySpawner.UnspawnEnemy(gameObject);
    }

    public void OnNotified(ShipDamageable notifier)
    {
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsPerDeath);
            DisableTimer.StartTimer(TimeToDisableAfterDeath);
        }
    }

    private void Update() 
    {
        if(!WasSetuped || _chaserShipDamage.IsDead())
            return;

        Chase();
    }

    private void Chase()
    {
        _chaserShipMovement.MoveForward();

        if(!_chaserShipRotator.IsRotating())
        {
            Vector2 dir = new Vector2()
            {
                x = _chaserShipMovement.Position.x > _player.Position.x ? -1 : 1,            
                y = _chaserShipMovement.Position.y > _player.Position.y ? -1 : 1
            };

            _chaserShipRotator.StartRotation(dir);
        }
    }
    
    private void Explode()
    {
        if(!_chaserShipDamage.IsDead())
        {
            _player.GetShipDamageable().TakeDamage(DamageValue);
            _chaserShipDamage.TakeDamage(_chaserShipDamage.GetMaxHealthy());
        }
    }
}