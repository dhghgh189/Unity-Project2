using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveIdle : BaseState<WaveFSM>
{
    public WaveIdle(WaveFSM owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter WaveIdle");
    }
}
