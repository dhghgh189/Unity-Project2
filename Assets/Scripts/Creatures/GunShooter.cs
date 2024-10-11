using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject[] muzzleEffects;
    [SerializeField] float fireInterval;
    [SerializeField] float fireDistance;
    [SerializeField] LayerMask whatIsTarget;

    Coroutine _fireRoutine;

    float _nextFireTime;

    void Awake()
    {
        _nextFireTime = 0;
    }

    public void StartFire()
    {
        if (_fireRoutine != null)
            return;

        _fireRoutine = StartCoroutine(FireRoutine());
    }

    public void StopFire()
    {
        if (_fireRoutine == null)
            return;

        StopCoroutine(_fireRoutine);
        _fireRoutine = null;
    }

    IEnumerator FireRoutine()
    {
        int iRand;
        while (true)
        {
            if (Time.time < _nextFireTime)
            {
                yield return null;
                continue;
            }

            iRand = Random.Range(0, muzzleEffects.Length);

            // temp : 풀링 필요
            GameObject muzzleFlash = Instantiate(muzzleEffects[iRand], muzzleTransform.position, muzzleTransform.rotation);
            muzzleFlash.transform.SetParent(muzzleTransform);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, fireDistance, whatIsTarget))
            {
                BaseCreature creature = hit.collider.GetComponent<BaseCreature>();
                if (creature != null)
                {
                    creature.TakeDamage(damage, GetComponent<BaseCreature>());
                }
            }

            _nextFireTime = Time.time + fireInterval;
        }
    }
}
