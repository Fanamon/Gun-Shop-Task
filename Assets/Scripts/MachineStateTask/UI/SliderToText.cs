using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderToText : MonoBehaviour
{
    [SerializeField] private string _settingName;
    [SerializeField] Slider _slider;
    [SerializeField] private TMP_Text _text;

    public void SetSettingValue()
    {
        _text.text = $"{_settingName} = {_slider.value:0.##}";
    }
}