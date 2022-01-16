using UnityEngine;

public class ShipRenderer : MonoBehaviour, IObserver<ShipDamageable>
{
    [SerializeField] ShipSpriteSheetSO _spriteSheetSO;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] ShipDamageable _shipDamageableToObserver;

    private void Awake()
    {
        if(_shipDamageableToObserver != null)
        {
            _shipDamageableToObserver.AddListener(this);
            UpdateSpriteWithStatus(_shipDamageableToObserver.GetCurrentStatus());
        }
    }
    
    private void OnDestroy() => _shipDamageableToObserver?.RemoveListener(this);

    public void OnNotified(ShipDamageable notifier)
    {
        UpdateSpriteWithStatus(notifier.GetCurrentStatus());
    }

    private void UpdateSpriteWithStatus(ShipDamageable.DamageStatus status)
    {
        _renderer.sprite = _spriteSheetSO.GetDamageSprite(status);
    }
}