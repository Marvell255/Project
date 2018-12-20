using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Numbers : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public TMP_InputField InputField;

    private void Start()
    {
    }

    private void OnGUI()
    {
        var x = 290f;
        if (GUI.Button(new Rect(x, 10, 150, 36), "100-200"))
        {
            Numbers100200();
        }

        if (GUI.Button(new Rect(x, 50, 150, 36), "200-100"))
        {
            Numbers200100();
        }

        if (GUI.Button(new Rect(x, 100, 150, 36), "0-50"))
        {
            Numbers050();
        }

        if (GUI.Button(new Rect(x, 150, 150, 36), "0-100"))
        {
            Numbers0100();
        }

        if (GUI.Button(new Rect(x, 150, 150, 36), "1-100"))
        {
            Numbers1100();
        }

        /////////////////////////
        x = 460f;

        if (GUI.Button(new Rect(x, 10, 150, 36), "String rev"))
        {
            StringRev();
        }

        if (GUI.Button(new Rect(x, 50, 150, 36), "String num"))
        {
            StringNum();
        }

        if (GUI.Button(new Rect(x, 100, 150, 36), "String sum"))
        {
            StringNumSum();
        }

//        if (GUI.Button(new Rect(x, 150, 150, 36), "Num Sum"))
//        {
//            NumSum();
//        }

        /////////////////////////
        x = 620f;

        if (GUI.Button(new Rect(x, 10, 150, 36), "Arr sum"))
        {
            ArrSum();
        }

        if (GUI.Button(new Rect(x, 50, 150, 36), "Arr mul"))
        {
            ArrMul();
        }

        if (GUI.Button(new Rect(x, 150, 150, 36), "Fact"))
        {
            Fact();
        }
    }

    private void Fact()
    {
        var startTime = Time.realtimeSinceStartup;

//        var s = "";
        const int count = 10000;

        var q = 1;
        var w = 1;

        for (var i = 0; i < count; i++)
        {
            var lastValue = w + q;

//            s += " " + lastValue;

            q = w;
            w = lastValue;
        }

//        TextField.text = s;

        print(Time.realtimeSinceStartup - startTime);
    }

    private void Numbers100200()
    {
        var s = "";

        for (var i = 100; i < 200; i++)
            s += i + " ";

        TextField.text = s;
    }

    private void Numbers200100()
    {
        var s = "";

        for (var i = 200; i > 100; i--)
            s += i + " ";

        TextField.text = s;
    }

    private void Numbers050()
    {
        var s = "";

        for (var i = 2; i < 50; i += 2)
            s += i + " ";

        TextField.text = s;

//        var s = "";
//
//        for (var i = 0; i < 50; i++)
//            if (i % 2 == 0)
//                s += i + " ";
//
//        TextField.text = s;
    }

    private void Numbers0100()
    {
        var s = 0;

        for (var i = 0; i < 100; i++)
            s += i;

        TextField.text = "Sum: " + s;
    }

    private void Numbers1100()
    {
        var s = 1;

        for (var i = 1; i < 100; i++)
            s *= i;

        TextField.text = "Mul: " + s;
    }

    private void StringRev()
    {
        var s = InputField.text;
        var result = "";

        for (var i = s.Length - 1; i >= 0; i--)
            result += s[i];

        TextField.text = result;
    }

    private void StringNum()
    {
        var s = InputField.text;
        var result = new List<string>();

        foreach (var t in s)
        {
            var j = int.Parse(t.ToString());
            result.Add(j.ToString());
        }

        TextField.text = string.Join(", ", result.ToArray());
    }

    private void StringNumSum()
    {
        var s = InputField.text;
        var result = 0;

        foreach (var t in s)
        {
            result += int.Parse(t.ToString());
        }

        TextField.text = result.ToString();
    }

//    private void NumSum()
//    {
////        var s = InputField.text;
////        var num = int.Parse(s);
////        var result = 0;
////
////        for (var i = 0; i < s.Length; i++)
////        {
////            
////        }
////        
////
////        TextField.text = result.ToString();
//    }


    private void ArrSum()
    {
        var arr = new[] {10, 20, 30};

        var result = arr.Sum();

//        foreach (var t in arr)
//        {
//            result += t;
//        }

        TextField.text = result.ToString();
    }

    private void ArrMul()
    {
        var arr = new[] {10, 20, 30};

        var result = arr.Aggregate(1, (current, t) => current * t);

//        foreach (var t in arr)
//        {
//            result *= t;
//        }

        TextField.text = result.ToString();
    }
}