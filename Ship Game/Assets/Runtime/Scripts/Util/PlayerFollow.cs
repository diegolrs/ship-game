using UnityEngine;

[ExecuteAlways]
public class PlayerFollow : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] Vector2 _arm;

    private void Update() 
    {
        Vector3 target = _player.Position + _arm;
        target.z = transform.position.z;
        transform.position = target;
    }
}