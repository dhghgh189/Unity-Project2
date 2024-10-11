using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
    public int ID;
    public int StartSpawnCount;
    public int MaxSpawnCount;
    public float SpawnInterval;
    public Enemy EnemyPrefab;
}
