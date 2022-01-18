using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ChaserController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipMovement _chaserShipMovement;
    [SerializeField] ShipDamageable _chaserShipDamage;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float UnspawnTime => 2f;
    public EnemySpawner EnemySpawner { get; private set; }
    public GameMode GameMode {get; private set;}
    public Timer UnspawnTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        UnspawnTimer = GetComponent<Timer>();
        UnspawnTimer.AddListener(this);

        _chaserShipDamage.AddListener(this);

        this.EnemySpawner = spawner;
        this.GameMode = gameMode;
        _player = gameMode.GetPlayerShip();
        WasSetuped = true;

        StartCoroutine(ChasePlayerRoutine());
    }
    #endregion

    private void OnDestroy() 
    {
        UnspawnTimer?.RemoveListener(this);
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
        if(notifier.GetCurrentStatus() == ShipDamageable.DamageStatus.Dead)
        {
            GameMode.IncreaseScore(PointsPerDeath);
            UnspawnTimer.StartTimer(UnspawnTime);
        }
    }

    IEnumerator ChasePlayerRoutine()
    {
        //yield break;
        while(true)
        {
            Vector2 cur = transform.position;
            Vector2 player = _player.Position;

            float curRotation = _chaserShipMovement.Rotation;
            float targetAngle = player.x <= cur.x ? 90 : -90;
            float time = 2;

            //yield return new WaitForSeconds(2f);

            yield return StartCoroutine(_chaserShipMovement.RotateRoutine(curRotation, targetAngle, time));
        }
    }

    private void Explode()
    {
        if(_chaserShipDamage.GetCurrentStatus() != ShipDamageable.DamageStatus.Dead)
        {
            _chaserShipDamage.TakeDamage(_chaserShipDamage.GetMaxHealthy());
        }
    }
}