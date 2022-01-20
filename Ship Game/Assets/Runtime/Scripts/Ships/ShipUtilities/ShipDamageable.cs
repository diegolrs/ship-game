using UnityEngine;

public class ShipDamageable : ObserverNotifier<ShipDamageable>, IDamageable
{
    #region Damage Status
    private const float DamagedABitPercentage = 0.55f;
    private const float DamagedALotPercentage = 0.3f;

    public enum DamageStatus
    {
        FullHealthy,
        DamagedABit,
        DamagedALot,
        Dead
    }
    #endregion

    [SerializeField] int _maxHealthy;
    private DamageStatus _currentStatus;
    private int _currentHealthy;

    private void OnEnable() =>  ResetToDefault();

    public bool IsDead() => _currentStatus == DamageStatus.Dead;

    public int GetCurrentHealthy() => _currentHealthy;
    public int GetMaxHealthy() => _maxHealthy;
    public DamageStatus GetCurrentStatus() => _currentStatus;

    public void ResetToDefault()
    {
        _currentStatus = DamageStatus.FullHealthy;
        _currentHealthy = _maxHealthy;
    }

    public DamageStatus GetStatusWithPercentage(float percentage)
    {
        if(percentage <= 0)
            return DamageStatus.Dead;
        if(percentage <= DamagedALotPercentage)
            return DamageStatus.DamagedALot;
        if(percentage <= DamagedABitPercentage)
            return DamageStatus.DamagedABit;

        return DamageStatus.FullHealthy;
    }

    public void TakeDamage(int damageAmount)
    {
        if(_currentStatus != DamageStatus.Dead)
        {
            _currentHealthy = Mathf.Max(0, _currentHealthy - damageAmount);

            float percentage = (float)_currentHealthy / _maxHealthy;
            _currentStatus = GetStatusWithPercentage(percentage);

            NotifyListeners();
        }
    }
}