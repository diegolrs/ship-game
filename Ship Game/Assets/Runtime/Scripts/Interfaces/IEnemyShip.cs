public interface IEnemyShip
{
    int PointsToAddAfterBeKilled {get;}
    float DelayToDisableAfterBeKilled {get;}

    EnemySpawner EnemySpawner {get;}
    GameMode GameMode {get; }
    Timer DisableAfterBeKilledTimer {get;}

    void SetupShip(EnemySpawner spawner, GameMode gameMode);
}