using UnityEngine;

public class ShipAnimation : MonoBehaviour, IObserver<ShipDamageable>
{
    [SerializeField] ShipSpriteSheetSO _spriteSheetSO;
    [SerializeField] ShipDamageable _shipDamageableToObserver;

    [Header("Animation Objects")]
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] GameObject _smallFire;
    [SerializeField] GameObject _bigFire;
    [SerializeField] Animator _explosionAnimator;

    private const string ExplosionAnimation = "Explode";

    private void Awake()
    {
        if(_shipDamageableToObserver != null)
        {
            _shipDamageableToObserver.AddListener(this);
            UpdateSprite(_shipDamageableToObserver.GetCurrentStatus());
        }

        _smallFire.SetActive(false);
        _bigFire.SetActive(false);
    }
    
    private void OnDestroy() => _shipDamageableToObserver?.RemoveListener(this);

    public void OnNotified(ShipDamageable notifier)
    {
        var curStatus = notifier.GetCurrentStatus();

        UpdateSprite(curStatus);
        ProcessFireAnimation(curStatus);
        PlayExplosionAnimation();
    }

    private void UpdateSprite(ShipDamageable.DamageStatus status)
    {
        _renderer.sprite = _spriteSheetSO.GetDamageSprite(status);  
    }

    private void ProcessFireAnimation(ShipDamageable.DamageStatus status)
    {
        bool enableSmallFire = status == ShipDamageable.DamageStatus.DamagedABit
                              || status == ShipDamageable.DamageStatus.DamagedALot;

        bool enableBigFire = status == ShipDamageable.DamageStatus.DamagedALot;
        
        _smallFire.SetActive(enableSmallFire);
        _bigFire.SetActive(enableBigFire);
    }

    private void PlayExplosionAnimation()
    {
        _explosionAnimator?.SetTrigger(ExplosionAnimation);
    }
}