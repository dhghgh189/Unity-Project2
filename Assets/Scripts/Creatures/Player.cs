using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;

    GunShooter _shooter;
    int _coins;

    bool _isShooting;

    public Transform CenterPivot { get { return transform.parent.transform; } }

    protected override void Init()
    {
        base.Init();

        // Temp : 버튼 UI를 통한 상호작용 필요
        _shooter = GetComponent<GunShooter>();
        _isShooting = false;

        _coins = startCoin;
    }

    void Update()
    {
        if (GameManager.Instance.Wave.CurState != Enums.WaveState.Progress &&
            GameManager.Instance.Wave.CurState != Enums.WaveState.Clear)
        {
            if (_isShooting)
            {
                StopShoot();
            }           
            return;
        }
    }

    public void AddCoin(int amount)
    {
        _coins += amount;
        Debug.Log($"AddCoin : {amount}");
    }

    public void TryShoot()
    {
        _isShooting = true;
        _shooter.StartFire();
    }

    public void StopShoot()
    {
        _shooter.StopFire();

        if (_isShooting)
            _isShooting = false;
    }
}
