using UnityEngine;

public class SideTripleShoot : MonoBehaviour, ICanonBallAttack
{
    [field: SerializeField] public int DamagePerBall {get; private set;} = 10;
    [field: SerializeField] public float Speed {get; private set;} = 8;
    [field: SerializeField] public CanonBallGenerator CanonBallGenerator {get; private set;}

    [SerializeField] Transform _shipTransform;
    [SerializeField] Transform _positionReference;
    
    float SideAngle = Mathf.Cos(15 * Mathf.Deg2Rad);

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.T))
            Attack();
    }

    // TODO: FIX ROTATION
    public void Attack()
    { 
        Vector3 rot = _shipTransform.rotation.eulerAngles;
        Vector2 centerDirection = _shipTransform.Forward2d();
        Vector2 rotated = new Vector2(centerDirection.y, -centerDirection.x);

        Vector2 targetDirection = rotated;

        for(int i = -1; i <= 1; i++)
        {
            targetDirection.x = rotated.x + SideAngle * i;

            CanonBallGenerator.GenerateCanonBall(
                                        DamagePerBall,
                                        Speed,
                                        targetDirection,
                                        _positionReference.position,
                                        true
                                    );

        }
    }
}