using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    public override void Use()
    {
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.Heal((int)_value);
        }
    }
}
