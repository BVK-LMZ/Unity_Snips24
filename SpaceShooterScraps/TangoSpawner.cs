using System.Collections.Generic;
using UnityEngine.Splines;
using UnityEngine;
using System.Linq;

public class TangoSpawner : MonoBehaviour
{
    [SerializeField] List<EnemyTypeSO> _enemyTypes;
    [SerializeField] int _maxEnemies = 10;
    [SerializeField] float _spawnInterval = 2.0f;

    public List<SplineContainer> _splines = new List<SplineContainer>();
    EnemyFactory _enemyFactory;

    float _spawnTimer;
    int tangosSpawned;

    private void OnValidate()
    {
        _splines = FindObjectsOfType<SplineContainer>().ToList();
    }


    void SpawnEnemy()
    {
        if (_enemyTypes.Count == 0)
        {
            Debug.LogError("No enemy types available!");
            return;
        }

        if (_splines.Count == 0)
        {
            Debug.LogError("No splines available!");
            return;
        }

        EnemyTypeSO enemyType = _enemyTypes[UnityEngine.Random.Range(0, _enemyTypes.Count)];
        SplineContainer spline = _splines[UnityEngine.Random.Range(0, _splines.Count)];

        GameObject Tango = _enemyFactory.CreateEnemy(enemyType, spline);
        tangosSpawned++;
    }

    void Start() => _enemyFactory = new EnemyFactory(); // create factory 

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (tangosSpawned < _maxEnemies && _spawnTimer >= _spawnInterval)
        {
            SpawnEnemy();
            _spawnTimer = 0;
        }
    }
}
