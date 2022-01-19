using UnityEngine;

public interface ICanonBallAttack 
{
    int DamagePerBall { get; }
    float Speed { get; }
    void Attack(Vector2 direction, CanonBallGenerator canonBallGenerator);
}