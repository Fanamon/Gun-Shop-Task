using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private bool _purchased;

    [SerializeField] protected Bullet Bullet;

    public string Label => _label;
    public Sprite Icon => _icon;
    public int Price => _price;
    public bool Purchased => _purchased;

    public abstract void Shoot(Transform shootPoint);

    public void Buy()
    {
        _purchased = true;
    }
}
