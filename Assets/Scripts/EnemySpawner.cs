using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected List<WaveConfiguration> _waveConfigurations;

   [SerializeField] protected int _startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        var currentWave = _waveConfigurations[_startingWave];
        StartCoroutine(SpawningAllWaves());
        //Debug.Log(currentWave);
        //StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    protected IEnumerator SpawningAllWaves()
    {
        for (int waveIndex = _startingWave; waveIndex < _waveConfigurations.Count; waveIndex++)
        {
            var currentWave = _waveConfigurations[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    protected IEnumerator SpawnAllEnemiesInWave(WaveConfiguration waveConfiguration)
    {
        for (int enemyCount = 0; enemyCount < waveConfiguration.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy=Instantiate(
                waveConfiguration.GetEnemyPrefab(),
                waveConfiguration.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfiguration(waveConfiguration);
            yield return new WaitForSeconds(waveConfiguration.GetTimeBetweenSpawns());
        }
        
    }

}
