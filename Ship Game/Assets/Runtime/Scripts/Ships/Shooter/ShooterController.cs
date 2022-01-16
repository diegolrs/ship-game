using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] PlayerController _player;
    [SerializeField] float _minDistanceToShoot = 1.5f;

    [Header("Gizmos")]
    [Tooltip("Color to apply on Gizmos when player is out of the attack radius")]
    [SerializeField] Color _colorWhenCantShoot = Color.green;
    [Tooltip("Color to apply on Gizmos when player is inside the attack radius")]
    [SerializeField] Color _colorWhenCanShoot = Color.red;

    bool shouldShoot;

    private void OnDrawGizmos() 
    {
        Gizmos.color = shouldShoot ? _colorWhenCanShoot : _colorWhenCantShoot;
        Gizmos.DrawWireSphere(transform.position, _minDistanceToShoot);
    }

    private void Update()
    {
        shouldShoot = Vector2.Distance(transform.position, _player.Position) < _minDistanceToShoot;
        if(shouldShoot) ShootPlayer();
    }

    private void ShootPlayer()
    {
        Debug.Log("player should be shooted");
    }
}