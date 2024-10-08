using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float destroyTime;

    float _timer;

    void Start()
    {
        _timer = destroyTime;    
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            // temp : 풀링 필요
            Destroy(gameObject);
        }
    }
}
