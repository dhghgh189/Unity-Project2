using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    WaveFSM _waveFSM;
    public WaveFSM Wave { get { return _waveFSM; } }
    int _currentWaveIndex;

    Enums.GameState _curState;

    Player _player;
    public Player Player { get { return _player; } }

    [SerializeField] float readyTime;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _curState = Enums.GameState.Idle;
            _currentWaveIndex = 0;
            _waveFSM = new WaveFSM(readyTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        switch (_curState)
        {
            case Enums.GameState.Progress:
                _waveFSM.Update();
                break;
        }
    }

    public void SetPlayer(Player player)
    {
        _player = player;
        _waveFSM.Init();
    }

    void ChangeState(Enums.GameState state)
    {
        if (_curState != state)
        {
            _curState = state;
        }
    }

    public void StartWave()
    {
        _waveFSM.StartWave(_currentWaveIndex);
        _curState = Enums.GameState.Progress;
    }

    public void EndWave()
    {
        _curState = Enums.GameState.Idle;
    }

    void OnDisable()
    {
        _waveFSM.Clear();    
    }
}
