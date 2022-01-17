public interface ICanonBallAttack 
{
    int DamagePerBall { get; }
    float Speed { get; }
    CanonBallGenerator CanonBallGenerator {get; }
    void Attack();
}