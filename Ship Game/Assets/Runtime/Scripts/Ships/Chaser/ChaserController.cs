using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class ChaserController : MonoBehaviour, IEnemyShip, IObserver<Timer>, IObserver<ShipDamageable>
{
    [SerializeField] PlayerController _player;
    [SerializeField] ShipMovement _chaserShipMovement;
    [SerializeField] ShipDamageable _chaserShipDamage;

    private const int DamageValue = 30;

    private const float LeftTopAngle = 135;
    private const float LeftBottomAngle = -135 + 360;
    private const float RightTopAngle = 45;
    private const float RightBottomAngle = -45 + 360;

    private bool _canRotate;


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
        _player = gameMode.GetPlayerShip();

        _canRotate = true;
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

    private void Chase()
    {
        Vector2 cur = _chaserShipMovement.transform.position;
        Vector2 player = _player.Position;

        Vector2 dir = Vector2.zero;
        dir.x = cur.x > player.x ? -1 : 1;               
        dir.y= cur.y > player.y ? -1 : 1;

        float rotation = 0;

        if(dir.x > 0 && dir.y > 0)
            rotation = RightTopAngle;
        else if(dir.x > 0 && dir.y < 0)
            rotation = RightBottomAngle;
        else if(dir.x < 0 && dir.y > 0)
            rotation = LeftTopAngle;
        else if(dir.x < 0 && dir.y < 0)
            rotation = LeftBottomAngle;


        //transform.rotation = Quaternion.Euler( Vector3.forward * rotation ); // funciona
        
        _chaserShipMovement.MoveForward();

        if(!isRotating && Vector2.Distance(transform.position, _player.Position) >= 1.2f)
        {
            rotateTime = 0;
            initRotation = transform.rotation.eulerAngles.z;
            rotateTarget = rotation;
            isRotating = true;
        }
        else
        {
            float tRotation = LerpRotation(initRotation, rotateTarget, rotateTime);
            transform.rotation = Quaternion.Euler( Vector3.forward * tRotation );
            rotateTime += Time.deltaTime * 0.5f;

            isRotating = rotateTime < maxRotateTime;
        }

        //if(_canRotate)
          //  StartCoroutine(WaitRotation(rotation));
    }

    float initRotation = 0;
    float rotateTarget = 0;
    float rotateTime = 0;
    float maxRotateTime = 1;
    bool isRotating = false;

    float LerpRotation(float init, float end, float t)
    {
        t = Mathf.Clamp(t, 0, 1);
        float target = init + t*(end - init);
        return target;
    }

    IEnumerator WaitRotation(float targetRotation)
    {
        _canRotate = false;
        yield return StartCoroutine(_chaserShipMovement.RotateRoutine(targetRotation));
        _canRotate = true;
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