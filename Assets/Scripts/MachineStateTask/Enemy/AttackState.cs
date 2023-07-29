using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Enemy))]
public class AttackState : State
{
    [SerializeField] private float _delay;

    private Enemy _enemy;
    private Animator _animator;
    private int _damage;
    private float _lastAttackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _damage = _enemy.Damage;
    }

    private void Update()
    {
        if (_lastAttackTime <= 0 && Target != null)
        {
            Attack(Target);

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void Attack(Player target)
    {
        _animator.Play("Attack");

        target.ApplyDamage(_damage);
    }
}
