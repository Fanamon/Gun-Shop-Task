using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MoveState : State
{
    private Enemy _enemy;
    private float _speed;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _speed = _enemy.Speed;
    }

    private void Update()
    {
        if (Target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }
    }
}
