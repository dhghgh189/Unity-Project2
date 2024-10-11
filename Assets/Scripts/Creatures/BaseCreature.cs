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
    protected int HP 
    { 
        get { return _hp; } 
        set 
        {
            _hp = value; 
            OnHPChanged?.Invoke(_hp, _maxHp); 
        } 
    }

    public UnityAction OnDamaged;
    public UnityAction OnDead;

    public UnityAction<int, int> OnHPChanged;

    void Awake()
    {
        Init();    
    }

    protected virtual void Init()
    {
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _hp = _maxHp;
    }

    public virtual void TakeDamage(int damage, BaseCreature attacker)
    {
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Die(attacker);
        }
        else
        {
            OnDamaged?.Invoke();
        }
    }

    protected virtual void Die(BaseCreature attacker)
    {
        OnDead?.Invoke();

        // Ǯ�� �ʿ�?
        gameObject.SetActive(false);
    }

    public void SetAnimation(int animHash, float transitionDuration)
    {
        _anim.CrossFade(animHash, transitionDuration);
    }
}
