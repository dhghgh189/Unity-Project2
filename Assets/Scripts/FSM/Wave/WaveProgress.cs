using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveProgress : BaseState<WaveFSM>
{
    public WaveProgress(WaveFSM owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveProgress");
    }
}
