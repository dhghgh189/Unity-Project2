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

    void OnEnable()
    {
        HideWaveUI();

        GameManager.Instance.Wave.OnChangedRemainEnemies += UpdateRemainEnemies;
        GameManager.Instance.Wave.OnTimerChanged += UpdateTimer;
        GameManager.Instance.Wave.OnProgress += ShowGameUI;
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

    public void HideWaveUI()
    {
        imgCrossHair.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.Wave.OnChangedRemainEnemies -= UpdateRemainEnemies;
            GameManager.Instance.Wave.OnTimerChanged -= UpdateTimer;
            GameManager.Instance.Wave.OnProgress -= ShowGameUI;
        }
    }
}
