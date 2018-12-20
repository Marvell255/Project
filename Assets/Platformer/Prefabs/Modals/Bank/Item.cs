using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string _data = "empty";

    public TextMeshProUGUI ButtonLabel;

    public void Setup(string data)
    {
        _data = data;
        ButtonLabel.text = "Buy " + data;
    }

    public void OnClickBuy()
    {
        print("Buy: " + _data);
    }
}