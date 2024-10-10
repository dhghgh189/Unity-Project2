using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnInterval;

    // �ӽ� : Wave Data �����Ǹ� �Űܾ� ��
    [SerializeField] int startSpawnCount;
    [SerializeField] int maxSpawnCount;
    [SerializeField] Enemy enemyPrefab;

    Player _player;

    List<Enemy> _listEnemies;

    public UnityAction<int> OnSpawn;
    public UnityAction<int> OnDespawn;

    // �ӽ� : WaveFSM �����Ǹ� �Űܾ� ��
    public UnityAction<int> OnChangedRemainCount;

    int _currentWaveSpawnCount;

    // �ӽ� : WaveFSM �����Ǹ� �Űܾ� ��
    int _remainEnemyCount;

    float _nextSpawnTime;

    private void Awake()
    {
        _listEnemies = new List<Enemy>(maxSpawnCount);
        _remainEnemyCount = maxSpawnCount;
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

        OnChangedRemainCount?.Invoke(_remainEnemyCount);
    }

    void Update()
    {
        if (_player == null || _player.gameObject.activeInHierarchy == false)
            return;

        if (_currentWaveSpawnCount < maxSpawnCount && Time.time >= _nextSpawnTime)
        {
            Spawn();
            _nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void Spawn()
    {
        Vector3 randPos = _player.CenterPivot.position + Random.insideUnitSphere * spawnRadius;
        // Ǯ�� �ʿ�
        Enemy enemy = Instantiate(enemyPrefab, randPos, Quaternion.identity);
        enemy.SetTarget(_player);
        enemy.OnDead = null;
        enemy.OnDead += () =>
        {
            _listEnemies.Remove(enemy);
            _remainEnemyCount--;
            OnDespawn?.Invoke(_listEnemies.Count);
            OnChangedRemainCount?.Invoke(_remainEnemyCount);
        };

        _listEnemies.Add(enemy);
        _currentWaveSpawnCount++;
        OnSpawn?.Invoke(_listEnemies.Count);
    }
}
