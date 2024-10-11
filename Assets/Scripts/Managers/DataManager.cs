using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    [SerializeField] List<WaveSO> listWaveSO;

    Dictionary<int, WaveSO> waveDict = new Dictionary<int, WaveSO>();

    public Dictionary<int, WaveSO> WaveDict
    {
        get { return waveDict; }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadData()
    {
        LoadWaveData();

        Debug.Log("Data Load Complete");
    }

    void LoadWaveData()
    {
        WaveSO waveData;
        for (int i = 0; i < listWaveSO.Count; i++)
        {
            waveData = listWaveSO[i];
            if (waveDict.ContainsKey(waveData.ID))
            {
                Debug.LogError($"Wave ID duplicate! / ID : {waveData.ID}");
                Debug.LogError("Please Check Data!");
                return;
            }
            waveDict.Add(waveData.ID, waveData);
        }
    }
}
