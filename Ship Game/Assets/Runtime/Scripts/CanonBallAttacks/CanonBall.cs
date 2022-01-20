using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CanonBall : MonoBehaviour, IObserver<Timer>
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField] private Timer _lifeTimer;

    [Space(2), Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private TrailRenderer _trail;

    private const float LifeTime = 1.1f;
    private Collider2D _colliderToIgnore;

    private void Awake() 
    {
        GetComponent<Collider2D>().isTrigger = true;
        _lifeTimer.AddListener(this);
    }

    private void OnDestroy() => _lifeTimer?.RemoveListener(this);

    void Update()
    {
        transform.position += (Vector3)_direction * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool ignoreCollision = _colliderToIgnore != null 
                                && other.GetComponent<Collider2D>() == _colliderToIgnore;

        if(ignoreCollision)
            return;

        if(other.GetComponent<IDamageable>() is IDamageable damageable)
        {   
            damageable.TakeDamage(_damage);
            EnableShoot(false);
        }
    }

    public void IgnoreCollider(Collider2D toIgnore) => _colliderToIgnore = toIgnore;
    public void SetDirection(Vector2 dir) => _direction = dir;
    public void SetDamage(int damage) => _damage = damage;
    public void SetSpeed(float speed) => _speed = speed;

    public void EnableShoot(bool enable)
    {
        if(enable)
        {
            _lifeTimer.StartTimer(LifeTime);
            _trail.Clear();
            gameObject.SetActive(true);
        }
        else
        {
            _lifeTimer.DisabeTimer();
            gameObject.SetActive(false);
        }
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == _lifeTimer)
            EnableShoot(false);
    }
}