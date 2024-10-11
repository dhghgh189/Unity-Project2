using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] Vector3 camPos;

    Player _player;

    void Start()
    {
        _player = GameManager.Instance.Player;    
    }

    void Update()
    {
        if (_player == null)
            return;

        transform.position = _player.transform.position + camPos;
    }
}
