using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : BaseItem
{
    public override bool Use()
    {
        if (GameManager.Instance.Player.HP < GameManager.Instance.Player.MaxHP)
        {
            Debug.Log($"체력 {_value} 회복!");
            GameManager.Instance.Player.Heal((int)_value);
            return true;
        }
        else
        {
            return false;
        }
    }
}
