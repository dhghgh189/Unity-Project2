using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveFSM
{
    Enums.WaveState _curState;
    BaseState<WaveFSM>[] _states = new BaseState<WaveFSM>[(int)Enums.WaveState.Max];

    float _readyTime;
    int _currentWaveStartSpawnCount;
    int _currentWaveMaxSpawnCount;
    float _currentWaveSpawnInterval;
    Enemy _currentEnemyPrefab;
    int _currentWaveSpawnCount;
    int _remainEnemies;

    public float ReadyTime { get { return _readyTime; } }
    public int CurrentWaveStartSpawnCount { get { return _currentWaveStartSpawnCount; } }
    public int CurrentWaveMaxSpawnCount { get { return _currentWaveMaxSpawnCount; } }
    public float CurrentWaveSpawnInterval { get { return _currentWaveSpawnInterval; } }
    public Enemy CurrentEnemyPrefab { get { return _currentEnemyPrefab; } }
    public int CurrentWaveSpawnCount { get { return _currentWaveSpawnCount; } }
    public int RemainEnemies 
    { 
        get { return _remainEnemies; }
        set 
        { 
            _remainEnemies = value; 
            OnChangedRemainEnemies?.Invoke(_remainEnemies);
        } 
    }

    public UnityAction<int> OnChangedRemainEnemies;

    public WaveFSM(float readyTime)
    {
        _readyTime = readyTime;

        _states[(int)Enums.WaveState.Idle] = new WaveIdle(this);
        _states[(int)Enums.WaveState.Ready] = new WaveReady(this);
        _states[(int)Enums.WaveState.Progress] = new WaveProgress(this);

        _curState = Enums.WaveState.Idle;
        _states[(int)_curState].OnEnter();
    }

    public void Init()
    {
        SpawnManager.Instance.OnDespawn += DecreaseRemainEnemies;
    }

    public void StartWave(int waveIndex)
    {
        // data init
        // 값은 테스트를 위해 임시로 설정, data 객체 구현 필요
        _currentWaveStartSpawnCount = 5;
        _currentWaveMaxSpawnCount = 10;
        _currentWaveSpawnInterval = 4f;
        _currentEnemyPrefab = Resources.Load<Enemy>("Enemy1");

        // field init
        _currentWaveSpawnCount = 0;
        RemainEnemies = _currentWaveMaxSpawnCount;

        // change state
        ChangeState(Enums.WaveState.Ready);
    }

    public void Update()
    {
        _states[(int)_curState].OnUpdate();
    }

    public void DecreaseRemainEnemies()
    {
        RemainEnemies--;
    }

    public void ChangeState(Enums.WaveState state)
    {
        if (state == _curState)
            return;

        _states[(int)_curState].OnExit();
        _curState = state;
        _states[(int)_curState].OnEnter();
    }

    public void Spawn()
    {
        SpawnManager.Instance.Spawn(CurrentEnemyPrefab, GameManager.Instance.Player);
        _currentWaveSpawnCount++;
    }

    public void Clear()
    {
        if (SpawnManager.Instance != null)
            SpawnManager.Instance.OnDespawn -= DecreaseRemainEnemies;
    }
}
