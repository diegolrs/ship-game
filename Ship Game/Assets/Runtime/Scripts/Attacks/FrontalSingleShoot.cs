using UnityEngine;

public class FrontalSingleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 30;
    [field: SerializeField] public float Speed {get; private set;} = 5;
    [field: SerializeField] public CanonBallGenerator CanonBallGenerator {get; private set;}

    [SerializeField] Transform _shipTransform;
    [SerializeField] Transform _positionReference;

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.J))
            Attack();
    }

    public void Attack()
    {
        CanonBallGenerator.GenerateCanonBall(
                                                DamagePerBall,
                                                Speed,
                                                _shipTransform.Forward2d(),
                                                _positionReference.position,
                                                true
                                            );
    }
}