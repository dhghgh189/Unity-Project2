using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRemainEnemies;

    void OnEnable()
    {
        GameManager.Instance.Wave.OnChangedRemainEnemies += UpdateRemainEnemies;
    }

    public void UpdateRemainEnemies(int count)
    {
        txtRemainEnemies.text = $"Remain Enemies : {count}";
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Wave.OnChangedRemainEnemies -= UpdateRemainEnemies;
    }
}
