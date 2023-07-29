using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;

    private int _maxHealth;

    public int Health => _health;
    public float Speed => _speed;
    public int Damage => _damage;
    public Player Target { get; private set; }

    public event UnityAction Died;

    private void Awake()
    {
        _maxHealth = _health;
    }

    private void OnEnable()
    {
        _health = _maxHealth;
    }

    public void Init(Player target)
    {
        Target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Target.AddMoney(_reward);
            Died?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
