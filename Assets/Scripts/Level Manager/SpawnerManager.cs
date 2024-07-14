using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerManager : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("Attrinutes")]
    [SerializeField] private int _baseEnemies = 8;
    [SerializeField] private float _spawnRate = 0.5f;
    [SerializeField] private float _pauseBetwenWaves = 5f;
    [SerializeField] private float _scallingFactor = 0.75f;

    private int _currentWave = 0;
    private float _timeFromLastSpawn;
    private int _curEnemyCount;
    private int _enemyToSpawn;
    private bool _isSpawning = false;


    private void OnEnable () {
        Enemy.OnEnemyDied += CountCurEnemy;
    }
    private void OnDisable () { 
        Enemy.OnEnemyDied -= CountCurEnemy;
    }

    private void Start () {
        StartWave();
    }

    private void Update () {
        if (!_isSpawning) return;
        SpawnEnemy();

    }

    private void SpawnEnemy () {
        _timeFromLastSpawn += Time.deltaTime;
        if (_timeFromLastSpawn >= (1f / _spawnRate)) {
            GameObject enemyToSpawn = _enemyPrefabs[0];
            Instantiate(enemyToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
            _curEnemyCount++;
            _enemyToSpawn--;
            _timeFromLastSpawn = 0;
            if (_enemyToSpawn == 0) _isSpawning = false;
        }
    }
    private void StartWave () {
        _currentWave++;
        _isSpawning = true;
        _enemyToSpawn = EnemyPerWave();
    }

    private void CountCurEnemy () {
        _curEnemyCount--;
        if(_curEnemyCount == 0 && _enemyToSpawn == 0) Invoke("StartWave",_pauseBetwenWaves);
    }

    private int EnemyPerWave () {
        return Mathf.RoundToInt(_baseEnemies * Mathf.Pow(_currentWave, _scallingFactor));
    }
}
