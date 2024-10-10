using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime;
    int _damage;

    Rigidbody _rb;

    BaseCreature _owner;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
    }

    public void SetOwner(BaseCreature owner)
    {
        _owner = owner;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void Fire(Vector3 dir, float speed)
    {
        _rb.AddForce(dir * speed, ForceMode.Impulse);
        StartCoroutine(DestroyRoutine());
    }

    IEnumerator DestroyRoutine()
    {
        WaitForSeconds _destroyTime = new WaitForSeconds(destroyTime);

        yield return _destroyTime;

        // Ǯ�� �ʿ�
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseCreature creature = other.GetComponent<BaseCreature>();
        if (creature != null)
        {
            creature.TakeDamage(_damage, _owner);

            // Ǯ�� �ʿ�
            Destroy(gameObject);
        }
    }
}
