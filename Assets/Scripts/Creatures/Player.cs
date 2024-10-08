using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;

    GunShooter _shooter;

    int _coins;

    protected override void Init()
    {
        base.Init();

        // Temp : 버튼 UI를 통한 상호작용 필요
        _shooter = GetComponent<GunShooter>();

        _coins = startCoin;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _shooter.StartFire();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _shooter.StopFire();
            }
        }
    }

    public void AddCoin(int amount)
    {
        _coins += amount;
    }
}
