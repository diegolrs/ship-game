using UnityEngine;

public class ShipDamageable : ObserverNotifier<ShipDamageable>, IDamageable
{
    public enum DamageStatus
    {
        FullHealthy,
        DamagedABit,
        DamagedALot,
        Dead
    }

    #region Status Percentage
    private const float DamagedABitPercentage = 0.55f;
    private const float DamagedALotPercentage = 0.3f;
    #endregion

    [SerializeField] int _maxHealthy;
    int _currentHealthy;
    DamageStatus _currentStatus;

    private void Start() 
    {
        _currentStatus = DamageStatus.FullHealthy;
        _currentHealthy = _maxHealthy;
    }

    public DamageStatus GetCurrentStatus() => _currentStatus;

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

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.K))
            TakeDamage(10);
    }

    public void TakeDamage(int damageAmount)
    {
        if(_currentStatus != DamageStatus.Dead)
        {
            _currentHealthy -= damageAmount;

            float percentage = (float)_currentHealthy / _maxHealthy;
            _currentStatus = GetStatusWithPercentage(percentage);

            NotifyListeners();
        }
    }
}