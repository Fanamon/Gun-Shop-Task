using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private ObjectPool[] _pools;
    [SerializeField] private GameObject _waveSetting;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _shopButton;
    [SerializeField] private GameObject _nextGunButton;
    [SerializeField] private GameObject _previousGunButton;

    private bool _isAllEnemySpawned = false;
    private bool _isAllEnemyDied = false;

    private void OnEnable()
    {
        foreach (var pool in _pools)
        {
            pool.AllEnemiesDied += OnAllEnemyDied;
        }

        _spawner.AllEnemySpawned += OnAllEnemySpawned;
        _player.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        foreach (var pool in _pools)
        {
            pool.AllEnemiesDied -= OnAllEnemyDied;
        }

        _spawner.AllEnemySpawned -= OnAllEnemySpawned;
        _player.Died -= OnPlayerDied;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void OpenPanelWithoutChangingTimeScale(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ClosePanelWithoutChangingTimeScale(GameObject panel)
    {
        panel.SetActive(false);
    }

    private void OnAllEnemyDied()
    {
        _isAllEnemyDied = true;

        TryOpenWaveSetting();
    }

    private void OnAllEnemySpawned()
    {
        _isAllEnemySpawned = true;

        TryOpenWaveSetting();
    }

    private void TryOpenWaveSetting()
    {
        if (_isAllEnemySpawned && _isAllEnemyDied)
        {
            OpenPanelWithoutChangingTimeScale(_waveSetting);

            _isAllEnemySpawned = false;
            _isAllEnemyDied = false;
        }
    }

    private void OnPlayerDied()
    {
        ClosePanelWithoutChangingTimeScale(_shopButton);
        ClosePanelWithoutChangingTimeScale(_nextGunButton);
        ClosePanelWithoutChangingTimeScale(_previousGunButton);

        OpenPanelWithoutChangingTimeScale(_restartButton);
    }
}