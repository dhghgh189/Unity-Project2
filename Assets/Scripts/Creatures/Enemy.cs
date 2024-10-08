using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCreature
{
    [SerializeField] float moveSpeed;
    [SerializeField] int damage;
    [SerializeField] int coinAmount;

    [SerializeField] float chaseRange;
    [SerializeField] float attackRange;
    [SerializeField] float attackInterval;

    Player _target;
    Vector3 _startPos;
    Enums.EnemyState _curState;

    BaseState<Enemy>[] _states = new BaseState<Enemy>[(int)Enums.EnemyState.Max];

    public float ChaseRange { get { return chaseRange * chaseRange; } }
    public float AttackRange { get { return attackRange * attackRange; } }

    protected override void Init()
    {
        base.Init();

        _states[(int)Enums.EnemyState.Idle] = new EnemyIdle(this);
        _states[(int)Enums.EnemyState.Chase] = new EnemyChase(this);
        _states[(int)Enums.EnemyState.Attack] = new EnemyAttack(this);
        _states[(int)Enums.EnemyState.Return] = new EnemyReturn(this);

        _startPos = transform.position;
        _curState = Enums.EnemyState.Idle;
    }

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            _target = go.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (_target == null)
            return;

        _states[(int)_curState].OnUpdate();
    }

    protected override void Die(BaseCreature attacker)
    {
        Player player = attacker as Player;
        if (player != null)
        {
            player.AddCoin(coinAmount);
        }

        base.Die(attacker);
    }

    public void ChangeState(Enums.EnemyState state)
    {
        if (state == _curState)
            return;

        _states[(int)_curState].OnExit();
        _curState = state;
        _states[(int)_curState].OnEnter();
    }
}
