using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Layouts : MonoBehaviour
{
    public GameObject ButtonPrefab;

    private void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            var button = Instantiate(ButtonPrefab, transform);
            button.name = (i + 1).ToString();
            button.GetComponentInChildren<Text>().text = "Action " + (i + 1);
        }
    }
}