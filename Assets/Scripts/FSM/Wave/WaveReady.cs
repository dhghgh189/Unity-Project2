using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveReady : BaseState<WaveFSM>
{
    float _timer;
    public WaveReady(WaveFSM owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveReady");
        _timer = _owner.ReadyTime;

        for (int i = 0; i < _owner.CurrentWaveStartSpawnCount; i++)
        {
            _owner.Spawn();
        }
    }

    public override void OnUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _owner.ChangeState(Enums.WaveState.Progress);
        }
    }
}
