using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAttack : BaseState<Enemy>
{
    float _nextAttackTime;

    public EnemyAttack(Enemy owner) : base(owner) { _nextAttackTime = 0; }

    public override void OnEnter()
    {
        Debug.Log("OnEnter EnemyAttack");
    }

    public override void OnUpdate()
    {
        _owner.transform.LookAt(_owner.Target.CenterPivot);

        Vector3 toTarget = _owner.GetToTarget();
        float sqrMagnitude = toTarget.sqrMagnitude;

        if (sqrMagnitude > _owner.AttackRange)
        {
            _owner.ChangeState(Enums.EnemyState.Idle);
        }
        else if (Time.time >= _nextAttackTime)
        {
            // TODO : Attack
            _owner.SetAnimation(Define.HASH_ATTACK, 0.25f);

            // 풀링 필요
            Projectile projectile = Object.Instantiate(_owner.ProjectilePrefab, _owner.ShotPivot.position, Quaternion.identity);
            projectile.transform.LookAt(_owner.Target.CenterPivot);
            projectile.SetOwner(_owner);
            projectile.SetDamage(_owner.ShotDamage);
            projectile.Fire(projectile.transform.forward, _owner.ShotSpeed);

            _nextAttackTime = Time.time + _owner.GetRandomInterval();
        }
    }
}
