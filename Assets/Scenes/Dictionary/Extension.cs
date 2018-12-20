using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static void AddRigidbody2D(this GameObject obj)
    {
        obj.AddComponent<Rigidbody2D>();
    }

    public static void Reset(this Transform t)
    {
        t.position = Vector3.zero;
        t.rotation = Quaternion.identity;
        t.localScale = Vector3.zero;
    }
}