using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMyBundle : MonoBehaviour
{
    private IEnumerator Start()
    {
        const string path = "http://test.wmdreams.com.ua/heroes.unity3d";

        var www = new WWW(path);

        if (!string.IsNullOrEmpty(www.error))
        {
            print(www.error);
        }

        while (!www.isDone)
        {
//            print(www.progress);
            yield return null;
        }

        var bundle = www.assetBundle;
        var hero = bundle.LoadAsset<GameObject>("NewHero");
        Instantiate(hero, transform);
    }
}