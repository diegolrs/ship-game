using UnityEngine;

public class EnemySpawner : MonoBehaviour, IObserver<Timer>
{
    [Header("Required Components")]
    [SerializeField] GameMode _gameMode;
    [SerializeField] GameplaySettingsSO _gameplaySettingsSO;
    [SerializeField] ObjectPool _enemyPooler;
    [SerializeField] Timer _enemySpawnTimer;

    [Header("Spawn Parameters")]
    [SerializeField] GameObject[] _enemyPrefabs;
    [SerializeField] SpawnArea[] _spawnAreas;
    [SerializeField] int _spawnOnStartQuant;
    [SerializeField] int _maxEnemies;
    private const float MinDistanceToPlayer = 5f;
    
    private void Awake() => _enemySpawnTimer?.AddListener(this);
    private void OnDestroy() => _enemySpawnTimer?.RemoveListener(this);
    private void Start()
    {
        for(int i = 0; i < _spawnOnStartQuant; i++)
            SpawnEnemy();

        _enemySpawnTimer.StartTimer(_gameplaySettingsSO.EnemySpawnTime, Timer.TimerMode.Loop); 
    }   

    public void DisableAllEnemies() => _enemyPooler.DisableAllObjects();
    public void DisableSpawn() => _enemySpawnTimer.DisabeTimer();

    public void UnspawnEnemy(GameObject enemyObject)
    {
        enemyObject.SetActive(false);
    }

    private void SpawnEnemy()
    {
        if(_enemyPooler.QuantityEnabled() >= _maxEnemies)
        {
            Debug.LogWarning("Max enemies allowed reached");
            return;
        }

        var prefab = _enemyPrefabs.PickRandom<GameObject>(); 
        //_enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];

        var enemy = _enemyPooler.PoolObject(prefab, true);
        enemy.transform.position = ChooseRandomPosition();
        enemy.transform.parent = transform;
        enemy.SetActive(true);

        IEnemyShip ship = enemy.GetComponent<IEnemyShip>();
        ship.SetupShip(this, _gameMode);
    }
    
    private Vector2 ChooseRandomPosition()
    {
        Vector2 position = Vector2.zero;
        SpawnArea spawn;

        do{
            spawn = _spawnAreas.PickRandom<SpawnArea>();
            position = spawn.PickRandomPositionInside();
        }
        while(Vector2.Distance(position, _gameMode.GetPlayerController().Position) < MinDistanceToPlayer);

        return position;
    }
    
    public void OnNotified(Timer notifier) => SpawnEnemy();
}