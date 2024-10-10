using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCreature
{
    [SerializeField] int coinAmount;

    [SerializeField] float attackRange;
    [SerializeField] float minAttackInterval;
    [SerializeField] float maxAttackInterval;

    [Header("Attack")]
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] int shotDamage;
    [SerializeField] float shotSpeed;
    [SerializeField] Transform shotPivot;

    Player _target;
    Vector3 _startPos;
    Enums.EnemyState _curState;

    BaseState<Enemy>[] _states = new BaseState<Enemy>[(int)Enums.EnemyState.Max];

    public Player Target { get { return _target; } }
    public Transform TargetPivot { get { return _target.transform.parent.transform; } }
    public Vector3 ToTarget { get { return TargetPivot.position - transform.position; } }
    public float AttackRange { get { return attackRange * attackRange; } }
    public Projectile ProjectilePrefab {  get { return projectilePrefab; } }
    public int ShotDamage { get { return shotDamage; } }
    public float ShotSpeed { get { return shotSpeed; } }
    public Transform ShotPivot { get { return shotPivot; } }

    protected override void Init()
    {
        base.Init();

        _states[(int)Enums.EnemyState.Idle] = new EnemyIdle(this);
        _states[(int)Enums.EnemyState.Attack] = new EnemyAttack(this);

        _startPos = transform.position;
        _curState = Enums.EnemyState.Idle;
        _states[(int)_curState].OnEnter();
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
        if (_target == null || _target.gameObject.activeInHierarchy == false)
        {
            if (_curState != Enums.EnemyState.Idle)
                ChangeState(Enums.EnemyState.Idle);

            return;
        }

        _states[(int)_curState].OnUpdate();
    }

    public Vector3 GetToTarget()
    {
        return TargetPivot.position - transform.position;
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

    public float GetRandomInterval()
    {
        float interval = Random.Range(minAttackInterval, maxAttackInterval);
        return interval;
    }
}
