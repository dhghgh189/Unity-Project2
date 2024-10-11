using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : BaseState<Enemy>
{
    public EnemyIdle(Enemy owner) : base(owner) { }

    public override void OnEnter()
    {
        Debug.Log("OnEnter EnemyIdle");
        _owner.transform.LookAt(_owner.Target.CenterPivot);
        _owner.SetAnimation(Define.HASH_IDLE, 0.25f);
    }

    public override void OnUpdate()
    {
        Vector3 toTarget = _owner.GetToTarget();
        float sqrMagnitude = toTarget.sqrMagnitude;

        if (sqrMagnitude <= _owner.AttackRange)
        {
            _owner.ChangeState(Enums.EnemyState.Attack);
        }
    }
}
