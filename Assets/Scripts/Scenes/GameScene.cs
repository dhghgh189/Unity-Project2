using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScene : MonoBehaviour
{
    void OnEnable()
    {
        GameObject camObject = GameObject.FindGameObjectWithTag("MinimapCamera");
        if (camObject == null)
        {
            Debug.LogWarning("Can't find Minimap Camera!");
        }
        else
        {
            Camera camera = camObject.GetComponent<Camera>();
            GameManager.Instance.SetMinimapCamera(camera);
        }

        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.ResetGame();
        }
        else
        {
            GameObject go = GameObject.FindGameObjectWithTag("Player");
            if (go == null)
            {
                Debug.LogError("Can't find player!");
                return;
            }

            Player player = go.GetComponent<Player>();
            GameManager.Instance.SetPlayer(player);
        }
    }

    void Start()
    {
        GameManager.Instance.StartWave();
    }
}
