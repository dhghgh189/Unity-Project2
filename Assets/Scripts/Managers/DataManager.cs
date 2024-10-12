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
    [SerializeField] List<ItemSO> listItemSO;

    Dictionary<int, WaveSO> waveDict = new Dictionary<int, WaveSO>();
    Dictionary<int, ItemSO> itemDict = new Dictionary<int, ItemSO>();

    public Dictionary<int, WaveSO> WaveDict
    {
        get { return waveDict; }
    }

    public Dictionary<int, ItemSO> ItemDict
    {
        get { return itemDict; }
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
        LoadItemData();

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

    void LoadItemData()
    {
        ItemSO itemData;
        for (int i = 0; i < listItemSO.Count; i++)
        {
            itemData = listItemSO[i];
            if (itemDict.ContainsKey(itemData.ID))
            {
                Debug.LogError($"Item ID duplicate! / ID : {itemData.ID}");
                Debug.LogError($"Old Value : {itemDict[itemData.ID].Name}");
                Debug.LogError($"New Value : {itemData.Name}");
                Debug.LogError("Please Check Data!");
                return;
            }
            itemDict.Add(itemData.ID, itemData);
        }
    }
}
