using UnityEngine;

[CreateAssetMenu(fileName = "New ShipSpriteSheetSO", menuName = "Data/ShipSpriteSheetSO")]
public class ShipSpriteSheetSO : ScriptableObject
{
    [SerializeField] Sprite _fullHealthySprite;
    [SerializeField] Sprite _damagedABitSprite;
    [SerializeField] Sprite _damagedALotSprite;
    [SerializeField] Sprite _deadSprite;

    public Sprite GetDamageSprite(ShipDamageable.DamageStatus damageStatus)
    {
        return damageStatus switch
        {
            ShipDamageable.DamageStatus.FullHealthy => _fullHealthySprite,
            ShipDamageable.DamageStatus.DamagedABit => _damagedABitSprite,
            ShipDamageable.DamageStatus.DamagedALot => _damagedALotSprite,
            ShipDamageable.DamageStatus.Dead => _deadSprite,
            _ => null
        };
    }
    
    private void Awake() => hideFlags = HideFlags.DontUnloadUnusedAsset;
}