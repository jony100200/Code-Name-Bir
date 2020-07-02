using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected bool _looping = false;

    [SerializeField] protected int _startingWave = 0;

    [SerializeField] protected List<WaveConfiguration> _waveConfigurations;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawningAllWaves());
        } while (_looping);
    }

    protected IEnumerator SpawningAllWaves()
    {
        for (var waveIndex = _startingWave; waveIndex < _waveConfigurations.Count; waveIndex++)
        {
            var currentWave = _waveConfigurations[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    protected IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfiguration)
    {
        for (var enemyCount = 0; enemyCount < waveConfiguration.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfiguration.GetEnemyPrefab(),
                waveConfiguration.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfiguration(waveConfiguration);
            yield return new WaitForSeconds(waveConfiguration.GetTimeBetweenSpawns());
        }
    }
}