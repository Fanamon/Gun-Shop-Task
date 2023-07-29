using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Transform _shootPoint;

    private Gun _currentGun;
    private int _currentGunNumber = 0;
    private Animator _animator;

    private float _shotCooldown = 1;
    private float _cooldownCounter;

    public int Money { get; private set; }

    public event UnityAction<int> MoneyChanged;
    public event UnityAction Died;

    private void Start()
    {
        ChangeGun(_guns[_currentGunNumber]);
        _animator = GetComponent<Animator>();
        _currentGun = _guns[_currentGunNumber];
        _cooldownCounter = _shotCooldown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left) && _cooldownCounter >= _shotCooldown)
        {
            StartCoroutine(MakeShoot());

            _cooldownCounter = 0;
        }
        else if (_cooldownCounter < _shotCooldown)
        {
            _cooldownCounter += Time.deltaTime;
        }
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void BuyGun(Gun gun)
    {
        Money -= gun.Price;
        MoneyChanged?.Invoke(Money);
        _guns.Add(gun);
    }

    public void TakeNextGun()
    {
        if (_currentGunNumber == _guns.Count - 1)
        {
            _currentGunNumber = 0;
        }
        else
        {
            _currentGunNumber++;
        }

        ChangeGun(_guns[_currentGunNumber]);
    }

    public void TakePreviousGun()
    {
        if (_currentGunNumber == 0)
        {
            _currentGunNumber = _guns.Count - 1;
        }
        else
        {
            _currentGunNumber--;
        }

        ChangeGun(_guns[_currentGunNumber]);
    }

    private void ChangeGun(Gun gun)
    {
        _currentGun = gun;
    }

    private IEnumerator MakeShoot()
    {
        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(0.2f);

        _currentGun.Shoot(_shootPoint);
    }
}
