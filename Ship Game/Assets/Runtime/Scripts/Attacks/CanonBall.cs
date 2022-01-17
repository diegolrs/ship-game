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

    Collider2D _collider;

    private void Awake() 
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = false;
    }

    void Update()
    {
        transform.position += (Vector3)_direction * Time.deltaTime * _speed;
        _curLifeTime += Time.deltaTime;

        if(_curLifeTime >= LifeTime)
            EnableShoot(false);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<IDamageable>() is IDamageable damageable)
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

    public void DisableShoot() => gameObject.SetActive(false);
    public void SetDirection(Vector2 dir) => _direction = dir;
    public void SetDamage(int damage) => _damage = damage;
    public void SetSpeed(float speed) => _speed = speed;
}