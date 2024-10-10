using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    static SpawnManager _instance = null;
    public static SpawnManager Instance { get { return _instance; } }

    [SerializeField] float spawnRadius;

    List<Enemy> _listEnemies;

    public UnityAction OnSpawn;
    public UnityAction OnDespawn;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _listEnemies = new List<Enemy>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Spawn(Enemy enemyPrefab, Player player)
    {
        Vector3 randPos = player.CenterPivot.position + Random.insideUnitSphere * spawnRadius;
        // 풀링 필요
        Enemy enemy = Instantiate(enemyPrefab, randPos, Quaternion.identity);
        enemy.SetTarget(player);
        enemy.OnDead = null;
        enemy.OnDead += () =>
        {
            _listEnemies.Remove(enemy);
            OnDespawn?.Invoke();
        };

        _listEnemies.Add(enemy);
        OnSpawn?.Invoke();
    }
}
