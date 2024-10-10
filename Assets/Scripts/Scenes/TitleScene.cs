using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class TitleScene : MonoBehaviour
{
    [SerializeField] ARSession arSession;
    [SerializeField] ARSessionOrigin arSessionOrigin;

    void Awake()
    {
        if (arSession == null || arSessionOrigin == null)
        {
            Debug.Log("Init Failed... Please Check Field!");
            Application.Quit();
        }

        DontDestroyOnLoad(arSession);
        DontDestroyOnLoad(arSessionOrigin);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
