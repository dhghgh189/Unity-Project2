using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseCreature
{
    [SerializeField] int startCoin;
    [SerializeField] Renderer playerRenderer;

    GunShooter _shooter;

    List<BaseItem> _inventory;
    public List<BaseItem> Inventory { get { return _inventory; } }

    int _coin;
    public int Coin 
    { 
        get { return _coin; }
        set 
        { 
            _coin = value; 
            OnChangedCoin?.Invoke(_coin); 
        } 
    }

    bool _isShooting;

    public Transform CenterPivot { get { return transform.parent.transform; } }

    public UnityAction<int> OnChangedCoin;
    public UnityAction OnChangedInventory;

    Coroutine _invisibleRoutine;

    protected override void Init()
    {
        base.Init();

        // Temp : 버튼 UI를 통한 상호작용 필요
        _shooter = GetComponent<GunShooter>();

        _isShooting = false;
        _coin = startCoin;
        _inventory = new List<BaseItem>(Define.INVENTORY_SIZE);
    }

    public override void ResetVariables()
    {
        base.ResetVariables();

        _isShooting = false;
        _coin = startCoin;
        _inventory.Clear();
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

            if (_invisibleRoutine != null)
            {
                StopCoroutine(_invisibleRoutine);
                _invisibleRoutine = null;
            }

            return;
        }
    }

    public bool AddItem(BaseItem item)
    {
        if (_inventory.Count >= Define.INVENTORY_SIZE)
            return false;

        _inventory.Add(item);

        OnChangedInventory?.Invoke();

        return true;
    }

    public void UseItem(int index)
    {
        if (index >= _inventory.Count)
            return;

        if (_inventory[index].Use())
        {
            _inventory.RemoveAt(index);
            OnChangedInventory?.Invoke();
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

    public void Invisible(float time)
    {
        if (_invisibleRoutine != null)
            return;

        _invisibleRoutine = StartCoroutine(InvisibleRoutine(time));
    }

    IEnumerator InvisibleRoutine(float time)
    {
        int originalLayer = gameObject.layer;
        Color originalColor = playerRenderer.material.color;
        WaitForSeconds waitTime = new WaitForSeconds(time);

        gameObject.layer = Define.INVISIBLE_LAYER;
        playerRenderer.material.color = Color.red;

        yield return waitTime;

        gameObject.layer = originalLayer;
        playerRenderer.material.color = originalColor;

        _invisibleRoutine = null;
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
