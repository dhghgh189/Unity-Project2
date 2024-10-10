using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    WaveFSM _waveFSM;
    int _currentWaveIndex;

    Enums.GameState _curState;

    [SerializeField] float readyTime;

    void Awake()
    {
        _curState = Enums.GameState.Idle;
        _currentWaveIndex = 0;
        _waveFSM = new WaveFSM(readyTime);
    }

    void Update()
    {
        switch (_curState)
        {
            case Enums.GameState.Progress:
                _waveFSM.Update();
                break;
        }
    }

    void ChangeState(Enums.GameState state)
    {
        if (_curState != state)
        {
            _curState = state;
        }
    }

    public void StartWave()
    {
        _waveFSM.StartWave(_currentWaveIndex);
        _curState = Enums.GameState.Progress;
    }

    public void EndWave()
    {
        _curState = Enums.GameState.Idle;
    }
}
