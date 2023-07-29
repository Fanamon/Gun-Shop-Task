using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GunView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    private Gun _gun;

    public event UnityAction<Gun, GunView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        _sellButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    public void Render(Gun gun)
    {
        _gun = gun;

        _label.text = _gun.Label;
        _price.text = _gun.Price.ToString();
        _icon.sprite = _gun.Icon;
    }

    private void TryLockItem()
    {
        if (_gun.Purchased)
        {
            _sellButton.interactable = false;
        }
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_gun, this);
    }
}