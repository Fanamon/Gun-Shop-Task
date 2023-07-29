using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _capacity;

    private Coroutine _coroutine;
    private List<GameObject> _pool = new List<GameObject>();

    public GameObject Prefab => _prefab;

    public event UnityAction AllEnemiesDied;

    private void OnEnable()
    {
        foreach (var enemyObject in _pool)
        {
            enemyObject.GetComponent<Enemy>().Died += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemyObject in _pool)
        {
            enemyObject.GetComponent<Enemy>().Died -= OnEnemyDied;
        }
    }

    public void OnEnemyDied()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        
        _coroutine = StartCoroutine(FindAliveEnemy());
    }

    public GameObject GetObject(int index)
    {
        return _pool[index];
    }

    private IEnumerator FindAliveEnemy()
    {
        yield return new WaitForEndOfFrame();

        GameObject result = _pool.FirstOrDefault(listObject => listObject.activeSelf == true);

        if (result == null)
        {
            AllEnemiesDied?.Invoke();
        }
    }

    public void Initialize(Player target)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(_prefab, gameObject.transform);
            spawned.GetComponent<Enemy>().Init(target);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }

        OnEnable();
    }
}