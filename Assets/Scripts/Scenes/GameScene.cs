using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    void OnEnable()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go == null)
        {
            Debug.LogError("Can't find player!");
            return;
        }

        Player player = go.GetComponent<Player>();
        GameManager.Instance.SetPlayer(player);
        GameManager.Instance.StartWave();
    }
}
