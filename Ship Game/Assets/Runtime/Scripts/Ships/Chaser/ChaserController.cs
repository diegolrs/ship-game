using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ChaserController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipMovement _chaserShipMovement;
    [SerializeField] ShipDamageable _chaserShipDamage;

    private enum Rotations
    {
        LeftAngle = 180,
        RightAngle = 0,
        TopAngle = 90,
        BottomAngle = 270,

        LeftTopAngle = 135,
        LeftBottomAngle = 225,
        RightTopAngle = 45,
        RightBottomAngle = 315,

        NoDirection = 0
    }

    private const int DamageValue = 30;


    #region Enemy Ship Interface
    public int PointsPerDeath => 1;
    public float UnspawnTime => 2f;
    public EnemySpawner EnemySpawner { get; private set; }
    public GameMode GameMode {get; private set;}
    public Timer UnspawnTimer { get; private set; }
    public bool WasSetuped { get; private set; }

    public void SetupShip(EnemySpawner spawner, GameMode gameMode)
    {
        if(UnspawnTimer == null)
        {
            UnspawnTimer = GetComponent<Timer>();
            UnspawnTimer.AddListener(this);
        }

        UnspawnTimer.DisabeTimer();


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
        if(notifier.IsDead())
        {
            GameMode.IncreaseScore(PointsPerDeath);
            UnspawnTimer.StartTimer(UnspawnTime);
        }
    }

    private void Update() 
    {
        if(!WasSetuped || _chaserShipDamage.IsDead())
            return;

        Chase();
    }

    Rotations ChooseCloseRotation(Rotations angle, Rotations sameAngle)
    {
        float cur = transform.rotation.z;

        return Mathf.Abs((float)angle - cur) <= Mathf.Abs((float)sameAngle - cur)
               ? angle
               : sameAngle;
    }

    Rotations GetRotationToDirection(Vector2 dir)
    {
        float minDeltaX = 0.8f;
        float minDeltaY = 0.8f;
        if(Mathf.Abs(dir.x) <= minDeltaX) dir.x = 0;
        if(Mathf.Abs(dir.y) <= minDeltaY) dir.y = 0;

        bool goToRight = dir.x > 0;
        bool goToLeft = dir.x < 0;
        bool goUp = dir.y > 0;
        bool goDown = dir.y < 0;
        
        if(goUp && goToRight) return Rotations.RightTopAngle;
        if(goUp && goToLeft) return Rotations.LeftTopAngle;
        if(goDown && goToRight) return Rotations.RightBottomAngle;
        if(goDown && goToLeft) return Rotations.LeftBottomAngle;

        if(goUp) return Rotations.TopAngle;
        if(goDown) return Rotations.BottomAngle;
        if(goToLeft) return Rotations.LeftAngle;
        if(goToRight) return Rotations.RightAngle;

        return Rotations.NoDirection;
    }

    private void Chase()
    {
        _chaserShipMovement.MoveForward();

        if(!isRotating)
        {
            Vector2 dir = new Vector2()
            {
                x = _chaserShipMovement.Position.x > _player.Position.x ? -1 : 1,            
                y = _chaserShipMovement.Position.y > _player.Position.y ? -1 : 1
            };

            rotateTime = 0;
            initRotation = transform.GetZRotationInDegrees();
            rotateTarget = (float) GetRotationToDirection(dir);
            isRotating = true;
        }
        else
        {
            float tRotation = MathUtils.LerpClamped(initRotation, rotateTarget, rotateTime);
            _chaserShipMovement.SetRotation(tRotation);
            rotateTime += Time.deltaTime * 1f;

            isRotating = rotateTime < maxRotateTime;
        }
    }

    float initRotation = 0;
    float rotateTarget = 0;
    float rotateTime = 0;
    float maxRotateTime = 1;
    bool isRotating = false;
    
    private void Explode()
    {
        if(!_chaserShipDamage.IsDead())
        {
            _player.GetShipDamageable().TakeDamage(DamageValue);
            _chaserShipDamage.TakeDamage(_chaserShipDamage.GetMaxHealthy());
        }
    }
}