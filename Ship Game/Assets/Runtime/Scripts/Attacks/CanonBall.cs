using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CanonBall : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    [SerializeField, Min(0)] private int _damage;

    [Space(2), Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direction;

    private float _curLifeTime;
    private const float LifeTime = 1.1f;
    private Collider2D _colliderToIgnore;

    private void Awake() 
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Update()
    {
        transform.position += (Vector3)_direction * Time.deltaTime * _speed;
        _curLifeTime += Time.deltaTime;

        if(_curLifeTime >= LifeTime)
            EnableShoot(false);
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

    public void EnableShoot(bool enable)
    {
        if(enable)
        {
            _curLifeTime = 0;
            _trail.Clear();
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void IgnoreCollider(Collider2D toIgnore) => _colliderToIgnore = toIgnore;
    public void SetDirection(Vector2 dir) => _direction = dir;
    public void SetDamage(int damage) => _damage = damage;
    public void SetSpeed(float speed) => _speed = speed;
}