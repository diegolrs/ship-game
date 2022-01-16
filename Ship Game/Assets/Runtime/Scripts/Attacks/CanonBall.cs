using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CanonBall : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField, Min(0)] private int _damage;
    [SerializeField] private Vector2 _direction;

    Collider2D _collider;

    private void Awake() 
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = false;
    }

    void Update()
    {
        transform.position += (Vector3)_direction * Time.deltaTime * _speed;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.GetComponent<IDamageable>() is IDamageable damageable)
        {
            damageable.TakeDamage(_damage);
            DisableShoot();
        }
    }

    public void EnableShoot() => gameObject.SetActive(true);
    public void DisableShoot() => gameObject.SetActive(false);
    public void SetDirection(Vector2 dir) => _direction = dir;
    public void SetDamage(int damage) => _damage = damage;
}