using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;

    GunShooter _shooter;

    int _coin;
    public int Coin 
    { 
        get { return _coin; }
        private set 
        { 
            _coin = value; 
            OnChangedCoin?.Invoke(_coin); 
        } 
    }

    bool _isShooting;

    public Transform CenterPivot { get { return transform.parent.transform; } }

    public UnityAction<int> OnChangedCoin;

    protected override void Init()
    {
        base.Init();

        // Temp : 버튼 UI를 통한 상호작용 필요
        _shooter = GetComponent<GunShooter>();
        _isShooting = false;

        _coin = startCoin;
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
        Coin += amount;
    }

    public void Heal(int amount)
    {
        if ((HP + amount) > MaxHP)
        {
            HP = MaxHP;
        }
        else
        {
            HP += amount;
        }
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
