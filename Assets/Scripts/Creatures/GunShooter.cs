using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{
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

        Debug.Log("Start Fire");
        _fireRoutine = StartCoroutine(FireRoutine());
    }

    public void StopFire()
    {
        if (_fireRoutine == null)
            return;

        Debug.Log("Stop Fire");
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

            Debug.Log("Fire");

            iRand = Random.Range(0, muzzleEffects.Length);

            // temp : 풀링 필요
            GameObject muzzleFlash = Instantiate(muzzleEffects[iRand], muzzleTransform.position, muzzleTransform.rotation);
            muzzleFlash.transform.SetParent(muzzleTransform);

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit, fireDistance, whatIsTarget))
            {
                // temp : target의 TakeDamage를 호출 하여 피격 시켜야 함
                Destroy(hit.collider.gameObject);
            }

            _nextFireTime = Time.time + fireInterval;
        }
    }
}
