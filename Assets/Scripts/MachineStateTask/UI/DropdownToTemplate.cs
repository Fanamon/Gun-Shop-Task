using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropdownToTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private GameObject[] _templates;

    public GameObject Template { get; private set; }

    private void Start()
    {
        Template = _templates[0];
    }

    public void SetTemplate(int index)
    {
        Template = _templates[index];
    }
}
