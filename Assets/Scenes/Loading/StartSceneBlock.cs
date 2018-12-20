using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneBlock : MonoBehaviour
{
    public IEnumerator Initialize()
    {
        print("Start loading");

        PrintEnum();

        yield return new WaitForSeconds(2);

        print("End loading");
    }

    private static void PrintEnum()
    {
        foreach (var value in Enum.GetValues(typeof(Margin)))
        {
            print(value);
        }
    }

    private enum Margin
    {
        Left,
        Right,
        Top,
        Bottom
    }
}