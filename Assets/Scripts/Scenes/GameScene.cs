using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
        {
            Debug.LogError("Can't find player!");
            return;
        }

        GameManager.Instance.SetPlayer(go.GetComponent<Player>());
        GameManager.Instance.StartWave();
    }
}
