using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Game : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtRemainEnemies;

    void OnEnable()
    {
        SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>();
        if (spawnManager == null)
            return;

        spawnManager.OnChangedRemainCount += UpdateRemainEnemies;
    }

    public void UpdateRemainEnemies(int count)
    {
        txtRemainEnemies.text = $"Remain Enemies : {count}";
    }

    private void OnDisable()
    {
        SpawnManager spawnManager = FindAnyObjectByType<SpawnManager>();
        if (spawnManager == null)
            return;

        spawnManager.OnChangedRemainCount -= UpdateRemainEnemies;
    }
}
