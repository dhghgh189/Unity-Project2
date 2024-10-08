using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;

    int _coins;

    protected override void Init()
    {
        base.Init();

        _coins = startCoin;
    }
}
