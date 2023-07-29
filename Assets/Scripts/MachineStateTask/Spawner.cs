using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private DropdownToTemplate _dropdownToTemplate;
    [SerializeField] private Player _target;
    [SerializeField] private Slider _delaySlider;
    [SerializeField] private Slider _countSlider;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ObjectPool[] _pools;

    private List<Wave> _waves = new List<Wave>();
    private Wave _currentWave;
    private int _currentWaveNumber = 0;

    public event UnityAction AllEnemySpawned;

    private void Awake()
    {
        foreach (var pool in _pools)
        {
            pool.Initialize(_target);
        }
    }

    private void OnEnable()
    {
        _target.Died += OnTargetDied;
    }

    private void OnDisable()
    {
        _target.Died -= OnTargetDied;
    }

    public void NextWave()
    {
        SetWave(_currentWaveNumber++);

        StartCoroutine(Spawn());
    }

    private void OnTargetDied()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Spawn()
    {
        ObjectPool spawnPool = GetPoolChosenByDropdown();

        for (int i = 0; i < _currentWave.Count; i++)
        {
            SetEnemy(spawnPool, i);

            yield return new WaitForSeconds(_currentWave.Delay);
        }

        AllEnemySpawned?.Invoke();
    }

    private ObjectPool GetPoolChosenByDropdown()
    {
        foreach (var pool in _pools)
        {
            if (pool.Prefab == _dropdownToTemplate.Template)
            {
                return pool;
            }
        }

        throw new NullReferenceException();
    }

    private void SetEnemy(ObjectPool pool, int index)
    {
        GameObject enemy = pool.GetObject(index);
        enemy.SetActive(true);
        enemy.transform.position = _spawnPoint.position;
    }

    private void SetWave(int index)
    {
        _waves.Add(new Wave(_dropdownToTemplate.Template, _delaySlider.value, (int)_countSlider.value));
        _currentWave = _waves[index];
    }
}

public class Wave
{
    public Wave(GameObject template, float delay, int count)
    {
        Template = template;
        Delay = delay;
        Count = count;
    }

    public GameObject Template { get; private set; }
    public float Delay { get; private set; }
    public int Count { get; private set; }
}
