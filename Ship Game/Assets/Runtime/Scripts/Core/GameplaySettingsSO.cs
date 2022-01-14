using UnityEngine;

[CreateAssetMenu(fileName = "New Gameplay Settings", menuName = "Data/Settings/Gameplay Settings")]
public class GameplaySettingsSO : ScriptableObject
{
    [field: SerializeField, Min(1)] public float MinGameSessionTime { get; private set; } = 1;
    [field: SerializeField, Min(0)] public float MaxGameSessionTime { get; private set; }
    [field: SerializeField, Min(0)] public float MinEnemySpawnTime { get; private set; } = 0;
    [field: SerializeField, Min(0)] public float MaxEnemySpawnTime { get; private set; }

    [field: SerializeField, Min(0)] public float GameSessionTime { get; private set; }
    [field: SerializeField, Min(0)] public float EnemySpawnTime { get; private set; }


    private void Awake() => hideFlags = HideFlags.DontUnloadUnusedAsset;


    public void SetGameSessionTime(float value)
    {
        GameSessionTime = Mathf.Clamp(MinGameSessionTime, value, MaxGameSessionTime);
    } 

    public void SetEnemySpawnTime(float value)
    {
        EnemySpawnTime = Mathf.Clamp(MinEnemySpawnTime, value, MaxEnemySpawnTime);
    } 
}