using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCreature : MonoBehaviour
{
    protected CapsuleCollider _collider;
    protected Animator _anim;

    [SerializeField]
    protected int _maxHp;
    protected int _hp;

    public UnityAction OnDamaged;
    public UnityAction OnDead;

    void Awake()
    {
        Init();    
    }

    protected virtual void Init()
    {
        _collider = GetComponent<CapsuleCollider>();

        _hp = _maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _hp = 0;
            Die();
        }
        else
        {
            OnDamaged?.Invoke();
        }
    }

    protected virtual void Die()
    {
        OnDead?.Invoke();
        gameObject.SetActive(false);
    }
}
