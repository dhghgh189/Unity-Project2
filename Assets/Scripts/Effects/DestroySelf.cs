using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] bool useDisable;

    float _timer;

    void OnEnable()
    {
        _timer = destroyTime;    
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            // Ç®¸µ 

            if (PoolManager.Instance.ContainsKey(gameObject.name) == false)
            {
                if (useDisable)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if (PoolManager.Instance.Push(gameObject) == false)
            {
                if (useDisable)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Destroy(gameObject);
                }
            }   
        }
    }
}
