using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchLight : MonoBehaviour
{
    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
        InvokeRepeating("RandomColor", 0, 3f);
    }

    private Color _color;

    private void Update()
    {
        _light.color = Color.Lerp(_light.color, _color, .04f);
    }

    private void RandomColor()
    {
        _color = Random.ColorHSV();
        _color.a = 1;
    }
}

