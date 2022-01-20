using UnityEngine;

[RequireComponent(typeof(Timer))]
public class FrontalSingleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 30;
    [field: SerializeField] public float CanonBallSpeed {get; private set;} = 5;
    [field: SerializeField] public float AttackCoolDown { get; private set; } = 2f;
    [field: SerializeField] public bool IsWaitingCoolDownEnds { get; private set; }
    public Timer CoolDownTimer { get; private set; }

    [SerializeField] Transform _positionReference;
    [SerializeField] Collider2D _colliderToIgnore;

    private void Awake()
    {
        CoolDownTimer = GetComponent<Timer>();
        CoolDownTimer?.AddListener(this);
    }

    private void OnDestroy() => CoolDownTimer?.RemoveListener(this);

    public void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator)
    {
        canonBallGenerator.GenerateCanonBall(
                                                DamagePerBall,
                                                CanonBallSpeed,
                                                direction,
                                                _positionReference.position,
                                                true,
                                                _colliderToIgnore
                                            );

        CoolDownTimer.StartTimer(AttackCoolDown);
        IsWaitingCoolDownEnds = true;
    }

    public void OnNotified(Timer notifier)
    {
        if(notifier == CoolDownTimer)
            IsWaitingCoolDownEnds = false;
    }
}