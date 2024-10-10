using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        GameManager gm = FindAnyObjectByType<GameManager>();
        gm.StartWave();
    }
}
