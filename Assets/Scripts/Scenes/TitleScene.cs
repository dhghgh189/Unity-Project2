using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class TitleScene : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
