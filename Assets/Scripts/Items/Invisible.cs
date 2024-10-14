using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Invisible : BaseItem
{
    public override bool Use()
    {
        if (GameManager.Instance.Player.gameObject.layer == Define.INVISIBLE_LAYER)
            return false;

        GameManager.Instance.Player.Invisible(_value);
        return true;
    }
}
