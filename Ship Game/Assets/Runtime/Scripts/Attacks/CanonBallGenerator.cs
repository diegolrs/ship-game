using UnityEngine;

public class CanonBallGenerator : MonoBehaviour
{
    [SerializeField, Range(0, 20)] int _preGenerateQuant;
    [SerializeField] CanonBall _canonBallPrefab;
    [SerializeField] ObjectPool _canonBallPooler;

    private void Start() 
    {
        for(int i = 0; i < _preGenerateQuant; i++)
        {
            var ball = GenerateCanonBall(0, 0, Vector2.zero, Vector2.zero, false);
            ball.EnableShoot(false);
        }
    }

    public CanonBall GenerateCanonBall(int damage, float speed, Vector2 dir, Vector2 pos, bool enabled)
    {
        var canonBall = _canonBallPooler.PoolObject(_canonBallPrefab.gameObject).GetComponent<CanonBall>();

        canonBall.transform.position = pos;  

        canonBall.SetDamage(damage);
        canonBall.SetSpeed(speed);
        canonBall.SetDirection(dir);

        canonBall.EnableShoot(enabled);

        return canonBall;
    }
}