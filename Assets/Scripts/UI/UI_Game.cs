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

    void OnEnable()
    {
        HideGameUI();
        clearPanel.SetActive(false);

        GameManager.Instance.Wave.OnChangedRemainEnemies += UpdateRemainEnemies;
        GameManager.Instance.Wave.OnTimerChanged += UpdateTimer;
        GameManager.Instance.Wave.OnProgress += ShowGameUI;
        GameManager.Instance.Wave.OnWaveClear += WaveClear;
        GameManager.Instance.Wave.OnWaveEnd += WaveEnd;
    }

    void Start()
    {
        GameManager.Instance.Player.OnHPChanged += UpdateHP;
        GameManager.Instance.Player.OnDead += HideGameUI;
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
    }

    public void HideGameUI()
    {
        imgCrossHair.gameObject.SetActive(false);
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

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Wave.OnChangedRemainEnemies -= UpdateRemainEnemies;
            GameManager.Instance.Wave.OnTimerChanged -= UpdateTimer;
            GameManager.Instance.Wave.OnProgress -= ShowGameUI;
            GameManager.Instance.Wave.OnWaveClear += WaveClear;
            GameManager.Instance.Wave.OnWaveEnd -= WaveEnd;

            if (GameManager.Instance.Player != null)
            {
                GameManager.Instance.Player.OnHPChanged -= UpdateHP;
                GameManager.Instance.Player.OnDead -= HideGameUI;
            }
        }
    }
}
