using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave")]
public class EnemyWave : ScriptableObject
{
    [SerializeField] internal float SpawnTime;
    [SerializeField] internal Enemy[] EnemiesToSpawn;
    [SerializeField] internal Meteor[] MeteorsToSpawn;
    [SerializeField] internal bool IsBossWave;
}
