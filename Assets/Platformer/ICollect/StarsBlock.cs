using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StarsBlock : MonoBehaviour
{
    public GameObject Prefab;
    public int Count = 5;
    public Vector3 Direction = Vector3.zero;

    private void Start()
    {
        Generate(Count);
    }

    private void Generate(int count)
    {
        var cachedTransform = transform;

        for (var i = 0; i < count; i++)
            Instantiate(Prefab, cachedTransform.position + Direction * i, new Quaternion(), cachedTransform);
    }
}