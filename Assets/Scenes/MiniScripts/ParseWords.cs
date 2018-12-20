using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParseWords : MonoBehaviour
{
    private InputField _inputField;
    private Dictionary<string, int> _freq = new Dictionary<string, int>();
    public Text TextLabel;

    private void Start()
    {
        _inputField = GetComponent<InputField>();
        _inputField.onValueChanged.AddListener(value =>
        {
            _freq = GetFreq(value); 
            print(_freq);
        });
    }

    private Dictionary<string, int> GetFreq(string value)
    {
        var result = new Dictionary<string, int>();
        
        var words = value.Split(new[]{' '},StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (!result.ContainsKey(word))
                result.Add(word, 1);
            else
                result[word]++;
        }

        return result;
    }
}