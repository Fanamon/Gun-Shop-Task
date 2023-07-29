using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Player _player;
    [SerializeField] private GunView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _guns.Count; i++)
        {
            AddItem(_guns[i]);
        }
    }

    private void AddItem(Gun gun)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(gun);
    }

    private void OnSellButtonClick(Gun gun, GunView view)
    {
        TrySellGun(gun, view);
    }

    private void TrySellGun(Gun gun, GunView view)
    {
        if (gun.Price <= _player.Money)
        {
            _player.BuyGun(gun);
            gun.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}
