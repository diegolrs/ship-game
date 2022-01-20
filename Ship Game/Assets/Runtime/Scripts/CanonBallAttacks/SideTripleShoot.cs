using UnityEngine;

[RequireComponent(typeof(Timer))]
public class SideTripleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 10;
    [field: SerializeField] public float CanonBallSpeed {get; private set;} = 8;
    [field: SerializeField] public float AttackCoolDown { get; private set; } = 2f;
    [field: SerializeField] public bool IsWaitingCoolDownEnds { get; private set; }
    public Timer CoolDownTimer { get; private set; }

    [SerializeField] Transform _positionReference;
    [SerializeField] Collider2D _colliderToIgnore;
    
    const float AngleBetweenCanonBalls = 15 * Mathf.Deg2Rad;

    private void Awake()
    {
        CoolDownTimer = GetComponent<Timer>();
        CoolDownTimer?.AddListener(this);
    }
    
    private void OnDestroy() => CoolDownTimer?.RemoveListener(this);

    public void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator)
    { 
        float baseRotation = (_positionReference.GetZRotationInDegrees()) * Mathf.Deg2Rad;

        for(int i = -1; i <= 1; i++)
        {
            float angleOffset = AngleBetweenCanonBalls * i;

            var k_dir = new Vector2(
                                        MathUtils.CosOfSum(baseRotation, angleOffset), 
                                        MathUtils.SinOfSum(baseRotation, angleOffset)
                                    );

            canonBallGenerator.GenerateCanonBall(
                                        DamagePerBall,
                                        CanonBallSpeed,
                                        k_dir,
                                        _positionReference.position,
                                        true,
                                        _colliderToIgnore
                                    );
        }

        CoolDownTimer.StartTimer(AttackCoolDown);
        IsWaitingCoolDownEnds = true;
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == CoolDownTimer)
            IsWaitingCoolDownEnds = false;
    }
}