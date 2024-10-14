using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public static readonly int HASH_IDLE = Animator.StringToHash("Idle");
    public static readonly int HASH_ATTACK = Animator.StringToHash("Attack");

    public const int INVENTORY_SIZE = 3;

    public static readonly int INVISIBLE_LAYER = LayerMask.NameToLayer("Invisible");
}
