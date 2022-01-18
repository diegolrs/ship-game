using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField, Min(0)] float _sizeX = 1f;
    [SerializeField, Min(0)] float _sizeY = 1f;

    Vector2 Size => new Vector2(_sizeX, _sizeY);
    Vector2 MinBounds => (Vector2) transform.position - Size * 0.5f;
    Vector2 MaxBounds => (Vector2) transform.position + Size * 0.5f;

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, Size);
    }

    public Vector2 PickRandomPositionInside()
    {   
        return new Vector2(){
                                x = Random.Range(MinBounds.x, MaxBounds.x),
                                y = Random.Range(MinBounds.y, MaxBounds.y)
                            };
    }
}
