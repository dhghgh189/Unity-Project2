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
    [SerializeField] UI_Title uiTitle;
    [SerializeField] float loadTime;

    bool _bLoaded = false;
    float _timer;

    void Awake()
    {
        if (arSession == null || arSessionOrigin == null)
        {
            Debug.Log("Init Failed... Please Check Field!");
            Application.Quit();
        }

        DontDestroyOnLoad(arSession);
        DontDestroyOnLoad(arSessionOrigin);

        _timer = loadTime;
    }

    void Update()
    {
        if (_bLoaded == false)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _bLoaded = true;
                uiTitle.HideLoading();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
