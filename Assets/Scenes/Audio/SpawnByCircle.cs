using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnByCircle : MonoBehaviour
{
    public GameObject Prefab;
    public int Count = 64;
    public float Radius = 8;
    
    [Range(50, 1000)] public float Mult = 100;
    [Range(0, 1)] public float Lerp = .5f;

    private AudioSource _audioSource;
    private AudioListener _audioListener;

    public FFTWindow FftWindow = FFTWindow.Blackman;
    private float[] Samples;
    private GameObject[] Items;

    private void Start()
    {
        Samples = new float[Count];
        Items = new GameObject[Count];

        Generate(Count);

        _audioSource = GetComponent<AudioSource>();
        _audioListener = Camera.main.GetComponent<AudioListener>();
    }

    private void Generate(int count)
    {
        var cachedTransform = transform;

        for (var i = 0; i < count; i++)
            Items[i] = Instantiate(Prefab, GetPosition(i), new Quaternion(), cachedTransform);
    }

    private Vector3 GetPosition(int position)
    {
        var step = Math.PI * 2 / Count;

        return new Vector3((float) (Math.Sin(step * position) * Radius), 0,
            (float) (Math.Cos(step * position) * Radius));
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(Samples, 0, FftWindow);

        for (var index = 0; index < Items.Length; index++)
        {
            var item = Items[index];
            item.transform.position = Vector3.Lerp(item.transform.position, new Vector3(
                item.transform.position.x,
                Mult * Samples[index],
                item.transform.position.z), Lerp);
        }
    }
}