using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager Instance { get { return _instance; } }

    WaveFSM _waveFSM;
    public WaveFSM Wave { get { return _waveFSM; } }

    Enums.GameState _curState;

    Player _player;
    public Player Player { get { return _player; } }

    [SerializeField] int readyTime;

    Camera _minimapCamera;
    public Camera MinimapCamera { get { return _minimapCamera; } }

    public UnityAction OnRequestOpenShop;
    public UnityAction OnGameClear;
    public UnityAction OnGameOver;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            _curState = Enums.GameState.Idle;
            _waveFSM = new WaveFSM(readyTime);
            _waveFSM.OnWaveEnd += EndWave;
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
        if (player == null)
            return;

        _player = player;
        _player.OnDead += GameOver;
        _waveFSM.Init();
    }

    public void SetMinimapCamera(Camera camera)
    {
        _minimapCamera = camera;
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
        if (_curState == Enums.GameState.Progress)
            return;

        Debug.Log("GameManger StartWave");
        _waveFSM.StartWave();
        _curState = Enums.GameState.Progress;
    }

    public void EndWave()
    {
        Debug.Log("GameManager EndWave");
        _curState = Enums.GameState.Idle;

        if (_waveFSM.CurrentWaveIndex < DataManager.Instance.WaveDict.Count -1)
        {
            OnRequestOpenShop?.Invoke();
        }
        else
        {
            OnGameClear?.Invoke();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        _waveFSM.ChangeState(Enums.WaveState.Idle);
        _curState = Enums.GameState.Idle;
        OnGameOver?.Invoke();
    }

    public void Reset()
    {
        _player.Reset();
        _waveFSM.Reset();
    }

    void OnDisable()
    {
        _waveFSM.OnWaveEnd -= EndWave;
        _waveFSM.Clear();
        _player.OnDead -= GameOver;
    }
}
