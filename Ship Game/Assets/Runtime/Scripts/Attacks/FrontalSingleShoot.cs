using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FrontalSingleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 30;
    [field: SerializeField] public float Speed {get; private set;} = 5;

    [SerializeField] Transform _shipTransform;
    [SerializeField] Transform _positionReference;
    [SerializeField] Collider2D _attackOwner;

    private void Awake() 
    {
        _attackOwner = GetComponent<Collider2D>();
    }

    public void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator)
    {
        canonBallGenerator.GenerateCanonBall(
                                                DamagePerBall,
                                                Speed,
                                                direction,
                                                _positionReference.position,
                                                true,
                                                _attackOwner
                                            );
    }
}