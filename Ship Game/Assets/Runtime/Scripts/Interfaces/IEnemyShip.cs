public interface IEnemyShip
{
    int PointsPerDeath {get;}
    float TimeToDisableAfterDeath {get;}

    EnemySpawner EnemySpawner {get;}
    GameMode GameMode {get; }
    Timer DisableTimer {get;}

    bool WasSetuped {get;}
    void SetupShip(EnemySpawner spawner, GameMode gameMode);
}