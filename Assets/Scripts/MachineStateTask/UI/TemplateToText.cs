using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemplateToText : MonoBehaviour
{
    [SerializeField] private DropdownToTemplate _dropdownToTemplate;
    [SerializeField] private TMP_Text _text;

    private int _health;
    private float _speed;
    private int _damage;

    public void SetStats()
    {
        _health = _dropdownToTemplate.Template.GetComponent<Enemy>().Health;
        _speed = _dropdownToTemplate.Template.GetComponent<Enemy>().Speed;
        _damage = _dropdownToTemplate.Template.GetComponent<Enemy>().Damage;

        _text.text = $"Health = {_health}\n\nSpeed = {_speed}\n\nDamage = {_damage}";
    }
}