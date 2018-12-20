using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : MonoBehaviour
{
    public GameObject[] Targets;

    private void Awake()
    {
//        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(ZoomTargetsSecond());
    }

    private IEnumerator ZoomTargetsSecond()
    {
        foreach (var target in Targets)
        {
            print(target.name);

            while (Time.time < 12f && target.transform.localScale.x < 4.25)
            {
                target.transform.localScale += new Vector3(.1f, .1f, .1f);
                yield return new WaitForSeconds(0.1f);
            }

            print(target.name + " break");
        }
    }

//    private void OnGUI()
//    {
//        if (NextScene.Length > 0)
//            GUI.Label(new Rect(10, 2, 100, 20), NextScene);
//    }
//
//    public string NextScene = "";
//
//    private void Update()
//    {
//        var inputString = Input.inputString;
//
//        if (inputString != "")
//        {
//            NextScene += inputString;
//            _invokeTime = Time.time;
//            _invoked = false;
//        }
//
//        if (!_invoked)
//        {
//            if (Time.time - _invokeTime > 3)
//            {
//                _invoked = true;
//
//                StartScene(NextScene);
//
//                NextScene = "";
//            }
//        }
//    }
//
//    private float _invokeTime;
//    private bool _invoked = true;
//
//    private void StartScene(string nextScene)
//    {
//        if (nextScene.Length == 0)
//        {
//            _invoked = false;
//            return;
//        }
//
//
//        try
//        {
//            print("Try Load by index" + nextScene);
//            var index = int.Parse(nextScene);
//            SceneManager.LoadScene(index);
//            return;
//        }
//        catch (Exception e)
//        {
//            Console.WriteLine(e);
//        }
//
//        print("Try Load " + nextScene);
//        SceneManager.LoadScene(nextScene);
//    }
}