using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProgress : BaseState<WaveFSM>
{
    float _nextSpawnTime;

    public WaveProgress(WaveFSM owner) : base(owner) { _nextSpawnTime = 0; }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveProgress");
        _owner.OnProgress?.Invoke();
    }

    public override void OnUpdate()
    {
        if (GameManager.Instance.Player == null || GameManager.Instance.Player.gameObject.activeInHierarchy == false)
            return;

        if (_owner.CurrentWaveSpawnCount < _owner.CurrentWaveMaxSpawnCount && Time.time >= _nextSpawnTime)
        {
            _owner.Spawn();
            _nextSpawnTime = Time.time + _owner.CurrentWaveSpawnInterval;
        }
    }
}
