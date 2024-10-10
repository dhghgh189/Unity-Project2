using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnInterval;

    // 임시
    [SerializeField] int startSpawnCount;
    [SerializeField] int maxSpawnCount;

    // 임시
    [SerializeField] Enemy enemyPrefab;

    Player _player;

    List<Enemy> _listEnemies;

    public UnityAction<int> OnSpawn;
    public UnityAction<int> OnDespawn;

    float _nextSpawnTime;

    private void Awake()
    {
        _listEnemies = new List<Enemy>(maxSpawnCount);
        _nextSpawnTime = 0;
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
        {
            Debug.LogError("Can't find Player!");
            return;
        }

        _player = go.GetComponent<Player>();

        for (int i = 0; i < startSpawnCount; i++)
        {
            Spawn();
        }
    }

    void Update()
    {
        if (_player == null || _player.gameObject.activeInHierarchy == false)
            return;

        if (_listEnemies.Count < maxSpawnCount && Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void Spawn()
    {
        Vector3 randPos = _player.CenterPivot.position + Random.insideUnitSphere * spawnRadius;
        // 풀링 필요
        Enemy enemy = Instantiate(enemyPrefab, randPos, Quaternion.identity);
        enemy.SetTarget(_player);
        enemy.OnDead = null;
        enemy.OnDead += () =>
        {
            _listEnemies.Remove(enemy);
            OnDespawn?.Invoke(_listEnemies.Count);          
        };

        _listEnemies.Add(enemy);
        OnSpawn?.Invoke(_listEnemies.Count);
    }
}
