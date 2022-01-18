public interface IEnemyShip
{
    int PointsPerDeath {get;}
    float UnspawnTime {get;}

    EnemySpawner EnemySpawner {get;}
    GameMode GameMode {get; }
    Timer UnspawnTimer {get;}

    bool WasSetuped {get;}
    void SetupShip(EnemySpawner spawner, GameMode gameMode);
}