using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    [SerializeField] protected GameObject _enemyPrefab;
    [SerializeField] protected float _moveSpeed = 2f;
    [SerializeField] protected int _numberOfEnemies = 5;
    [SerializeField] protected GameObject _pathPrefab;
    [SerializeField] protected float _spawnRandomness = 0.3f;
    [SerializeField] protected float _timeBetweenSpawns = 0.5f;

    public GameObject GetEnemyPrefab()
    {
        return _enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in _pathPrefab.transform) waveWaypoints.Add(child);
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return _timeBetweenSpawns;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }

    public int GetNumberOfEnemies()
    {
        return _numberOfEnemies;
    }

    public float GetSpawnRandomness()
    {
        return _spawnRandomness;
    }
}