using UnityEngine;

public interface ICanonBallAttack : IObserver<Timer>
{
    int DamagePerBall { get; }
    float CanonBallSpeed { get; }

    float AttackCoolDown { get; }
    Timer CoolDownTimer { get; }
    bool IsWaitingCoolDownEnds { get; }

    void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator);
}