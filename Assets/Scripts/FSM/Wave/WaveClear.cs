using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveClear : BaseState<WaveFSM>
{
    float _elapsedTime;

    public WaveClear(WaveFSM owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveClear");
        _owner.OnWaveClear?.Invoke();
        _elapsedTime = 0;
    }

    public override void OnUpdate()
    {
        if (_elapsedTime < _owner.ReadyTime)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            _owner.OnWaveEnd?.Invoke();
            _owner.ChangeState(Enums.WaveState.Idle);
        }
    }
}
