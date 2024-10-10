using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;

    GunShooter _shooter;
    int _coins;

    public Transform CenterPivot { get { return transform.parent.transform; } }

    protected override void Init()
    {
        base.Init();

        // Temp : ��ư UI�� ���� ��ȣ�ۿ� �ʿ�
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
        Debug.Log($"AddCoin : {amount}");
    }
}
