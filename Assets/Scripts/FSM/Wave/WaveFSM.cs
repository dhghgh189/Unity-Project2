using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveFSM
{
    Enums.WaveState _curState;
    public Enums.WaveState CurState { get { return _curState; } }

    BaseState<WaveFSM>[] _states = new BaseState<WaveFSM>[(int)Enums.WaveState.Max];

    int _currentWaveIndex;
    public int CurrentWaveIndex
    {
        get { return _currentWaveIndex; }
        set
        {
            _currentWaveIndex = value;
            OnChangedWaveIndex?.Invoke(_currentWaveIndex);
        }
    }

    WaveSO _currentWaveData;
    public WaveSO CurrentWaveData { get { return _currentWaveData; } }

    int _readyTime;
    int _currentWaveSpawnCount;
    int _remainEnemies;

    public int ReadyTime { get { return _readyTime; } }
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

    public UnityAction<int> OnChangedWaveIndex;
    public UnityAction<int> OnChangedRemainEnemies;
    public UnityAction<int> OnTimerChanged;
    public UnityAction OnProgress;
    public UnityAction OnWaveClear;
    public UnityAction OnWaveEnd;

    public WaveFSM(int readyTime)
    {
        _currentWaveIndex = -1;

        _readyTime = readyTime;

        _states[(int)Enums.WaveState.Idle] = new WaveIdle(this);
        _states[(int)Enums.WaveState.Ready] = new WaveReady(this);
        _states[(int)Enums.WaveState.Progress] = new WaveProgress(this);
        _states[(int)Enums.WaveState.Clear] = new WaveClear(this);

        _curState = Enums.WaveState.Idle;
        _states[(int)_curState].OnEnter();
    }

    public void Init()
    {
        SpawnManager.Instance.OnDespawn += DecreaseRemainEnemies;
    }

    public void StartWave()
    {
        CurrentWaveIndex++;

        // data init
        if (DataManager.Instance.WaveDict.TryGetValue(CurrentWaveIndex, out _currentWaveData) == false)
        {
            Debug.LogError("Invalid Wave Index!");
            Debug.LogError("Please check Data!");
            return;
        }

        // field init
        _currentWaveSpawnCount = 0;
        RemainEnemies = _currentWaveData.MaxSpawnCount;

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
        SpawnManager.Instance.Spawn(_currentWaveData.EnemyPrefab);
        _currentWaveSpawnCount++;
    }

    public void Reset()
    {
        CurrentWaveIndex = -1;
    }

    public void Clear()
    {
        if (SpawnManager.Instance != null)
            SpawnManager.Instance.OnDespawn -= DecreaseRemainEnemies;
    }
}
