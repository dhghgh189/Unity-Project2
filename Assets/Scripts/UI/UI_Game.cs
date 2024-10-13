using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRemainEnemies;
    [SerializeField] TextMeshProUGUI txtTimer;
    [SerializeField] Image imgCrossHair;
    [SerializeField] GameObject clearPanel;
    [SerializeField] Image imgHpFill;
    [SerializeField] TextMeshProUGUI txtWaveIndex;
    [SerializeField] Button btnShoot;
    [SerializeField] TextMeshProUGUI txtCoins;
    [SerializeField] UI_Item[] uiItems;
    [SerializeField] UI_Shop shopPanel; 

    void OnEnable()
    {
        HideGameUI();
        shopPanel.gameObject.SetActive(false);
        clearPanel.SetActive(false);

        for (int i = 0; i < uiItems.Length; i++)
        {
            uiItems[i].Init(i);
            uiItems[i].gameObject.SetActive(false);
        }

        GameManager.Instance.Wave.OnChangedRemainEnemies += UpdateRemainEnemies;
        GameManager.Instance.Wave.OnTimerChanged += UpdateTimer;
        GameManager.Instance.Wave.OnProgress += ShowGameUI;
        GameManager.Instance.Wave.OnWaveClear += WaveClear;
        GameManager.Instance.Wave.OnWaveEnd += WaveEnd;
        GameManager.Instance.Wave.OnChangedWaveIndex += UpdateWaveIndex;
        GameManager.Instance.OnRequestOpenShop += ShowShopUI;
    }

    void Start()
    {
        GameManager.Instance.Player.OnHPChanged += UpdateHP;
        GameManager.Instance.Player.OnDead += HideGameUI;
        GameManager.Instance.Player.OnChangedCoin += UpdateCoin;
        GameManager.Instance.Player.OnChangedInventory += UpdateInventory;

        // Init
        UpdateHP(GameManager.Instance.Player.HP, GameManager.Instance.Player.MaxHP);
        UpdateCoin(GameManager.Instance.Player.Coin);
    }

    public void UpdateRemainEnemies(int count)
    {
        txtRemainEnemies.text = $"Remain Enemies : {count}";
    }

    public void UpdateTimer(int time)
    {
        if (time <= 0)
        {
            txtTimer.gameObject.SetActive(false);
        }
        else
        {
            if (txtTimer.gameObject.activeInHierarchy == false)
                txtTimer.gameObject.SetActive(true);

            txtTimer.text = $"Ready\n{time}";
        }
    }

    public void ShowGameUI()
    {
        imgCrossHair.gameObject.SetActive(true);
        btnShoot.gameObject.SetActive(true);
    }

    public void HideGameUI()
    {
        imgCrossHair.gameObject.SetActive(false);
        btnShoot.gameObject.SetActive(false);
    }

    public void ShowShopUI()
    {
        shopPanel.gameObject.SetActive(true);

        // hide는 shopPanel에 있는 Close 버튼을 통해 별도로 처리
    }

    public void WaveClear()
    {
        HideGameUI();
        clearPanel.SetActive(true);
    }

    public void WaveEnd()
    {
        clearPanel.SetActive(false);
    }

    public void UpdateHP(int hp, int maxHp)
    {
        imgHpFill.fillAmount = (float)hp / maxHp;
    }

    public void UpdateWaveIndex(int waveIndex)
    {
        txtWaveIndex.text = $"WAVE {waveIndex + 1}";
    }

    public void UpdateCoin(int coin)
    {
        txtCoins.text = $"{coin}";
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < uiItems.Length; i++)
        {
            if (i < GameManager.Instance.Player.Inventory.Count)
            {
                uiItems[i].SetInfo(GameManager.Instance.Player.Inventory[i].ID);
                uiItems[i].gameObject.SetActive(true);
            }
            else
            {
                uiItems[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Wave.OnChangedRemainEnemies -= UpdateRemainEnemies;
            GameManager.Instance.Wave.OnTimerChanged -= UpdateTimer;
            GameManager.Instance.Wave.OnProgress -= ShowGameUI;
            GameManager.Instance.Wave.OnWaveClear += WaveClear;
            GameManager.Instance.Wave.OnWaveEnd -= WaveEnd;
            GameManager.Instance.Wave.OnChangedWaveIndex -= UpdateWaveIndex;
            GameManager.Instance.OnRequestOpenShop += ShowShopUI;

            if (GameManager.Instance.Player != null)
            {
                GameManager.Instance.Player.OnHPChanged -= UpdateHP;
                GameManager.Instance.Player.OnDead -= HideGameUI;
                GameManager.Instance.Player.OnChangedCoin -= UpdateCoin;
                GameManager.Instance.Player.OnChangedInventory += UpdateInventory;
            }
        }
    }
}
