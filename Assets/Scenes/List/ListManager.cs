using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public Image Indicator;
    public Sprite Yes, No;
    public TMP_InputField WordInputText;

    [SerializeField] private List<string> _list = new List<string>();

    private void Start()
    {
        _list.Add("List");
        _list.Add("int");
        _list.Add("float");
        _list.Add("double");
        _list.Add("string");

        WordInputText.onValueChanged.AddListener(WordInputTextChanged);
    }

    private void WordInputTextChanged(string value)
    {
        if (_list.Contains(value))
        {
            _list.Remove(value);
            Indicator.sprite = Yes;
        }
        else
        {
            Indicator.sprite = No;
        }
    }
}