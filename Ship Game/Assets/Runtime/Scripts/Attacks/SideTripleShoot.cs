using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SideTripleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 10;
    [field: SerializeField] public float Speed {get; private set;} = 8;

    [SerializeField] Transform _shipTransform;
    [SerializeField] Transform _positionReference;
    [SerializeField] Collider2D _attackOwner;
    
    const float AngleOffset = 15 * Mathf.Deg2Rad;
    float SideAngle = Mathf.Cos(15 * Mathf.Deg2Rad);

    private void Awake() 
    {
        _attackOwner ??= GetComponent<Collider2D>();
    }

    public void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator)
    { 
        float baseRotation = (_positionReference.GetZRotationInDegrees()) * Mathf.Deg2Rad;

        for(int i = -1; i <= 1; i++)
        {
            float angleOffset = AngleOffset * i;

            var k_dir = new Vector2(
                                        MathUtils.CosOfSum(baseRotation, angleOffset), 
                                        MathUtils.SinOfSum(baseRotation, angleOffset)
                                    );

            canonBallGenerator.GenerateCanonBall(
                                        DamagePerBall,
                                        Speed,
                                        k_dir,
                                        _positionReference.position,
                                        true,
                                        _attackOwner
                                    );
        }
    }
}