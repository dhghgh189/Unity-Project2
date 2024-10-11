using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveReady : BaseState<WaveFSM>
{
    int _timer;
    float _elapsedTime;

    int Timer 
    { 
        get { return _timer; } 
        set 
        { 
            _timer = value; 
            _owner.OnTimerChanged?.Invoke(_timer); 
        } 
    }

    public WaveReady(WaveFSM owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveReady");
        _elapsedTime = 0;

        for (int i = 0; i < _owner.CurrentWaveStartSpawnCount; i++)
        {
            _owner.Spawn();
        }

        Timer = _owner.ReadyTime;
    }

    public override void OnUpdate()
    {
        if (_timer > 0)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= 1)
            {
                Timer--;
                _elapsedTime = 0;
            }          
        }
        else
        {
            _owner.ChangeState(Enums.WaveState.Progress);
        }
    }
}
