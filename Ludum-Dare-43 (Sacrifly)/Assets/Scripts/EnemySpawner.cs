using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyWave[] _waves;
    [SerializeField] private Transform[] _spawnPoints;

    private int _maxLoopCount = 10;

    private int _upcomingWaveIndex;
    private int _loopCount;
    private float _startTime;
    private float _elapsedTime;

    private Stats _stats;

    private void Awake()
    {
        _startTime = Time.time;

        _stats = FindObjectOfType<Stats>();
    }

    private void Update()
    {
        _elapsedTime = Time.time - _startTime;

        if (_elapsedTime > _waves[_upcomingWaveIndex].SpawnTime * (Mathf.Pow(0.8f, _loopCount)))
        {
            SpawnNextWave();
        }
    }

    private void SpawnNextWave()
    {
        var waveToSpawn = _waves[_upcomingWaveIndex];

        Transform oldSpawnPoint = null;
        Transform newSpawnPoint = null;

        // Spawn enemies
        foreach (var enemy in waveToSpawn.EnemiesToSpawn)
        {
            while (newSpawnPoint == oldSpawnPoint)
            {
                newSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            }

            Instantiate(enemy, newSpawnPoint.position, newSpawnPoint.rotation);
            oldSpawnPoint = newSpawnPoint;
        }

        oldSpawnPoint = null;
        newSpawnPoint = null;

        // Spawn meteors
        foreach (var meteor in waveToSpawn.MeteorsToSpawn)
        {
            while (newSpawnPoint == oldSpawnPoint)
            {
                newSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            }

            Instantiate(meteor, newSpawnPoint.position, newSpawnPoint.rotation);
            oldSpawnPoint = newSpawnPoint;
        }

        if (_upcomingWaveIndex + 1 >= _waves.Length)
        {
            Loop();
        }
        else
        {
            _stats.SetWaveCount(++_upcomingWaveIndex);
        }
    }

    private void Loop()
    {
        _upcomingWaveIndex = 0;
        _stats.SetWaveCount(0);

        if (_loopCount + 1 < _maxLoopCount)
        {
            _loopCount++;
        }

        // Fake increment
        _stats.IncrementLoopCount();
        _startTime = Time.time;
    }
}
