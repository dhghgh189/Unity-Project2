using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveFSM
{
    Enums.WaveState _curState;
    BaseState<WaveFSM>[] _states = new BaseState<WaveFSM>[(int)Enums.WaveState.Max];

    int _currentWaveMaxSpawnCount;
    int _remainEnemies;
    float _readyTime;

    public float ReadyTime { get { return _readyTime; } }

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

    public void StartWave(int waveIndex)
    {
        // data init

        // change state
        ChangeState(Enums.WaveState.Ready);
    }

    public void Update()
    {
        _states[(int)_curState].OnUpdate();
    }

    public void ChangeState(Enums.WaveState state)
    {
        if (state == _curState)
            return;

        _states[(int)_curState].OnExit();
        _curState = state;
        _states[(int)_curState].OnEnter();
    }
}
