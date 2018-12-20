using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void Update()
    {
        // Press the space key to start coroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadYourAsyncScene());
        }
    }

     IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        var asyncLoad = SceneManager.LoadSceneAsync("HeavyScene", LoadSceneMode.Additive);

//        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f)
            {
//                Debug.Break();
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        var startSceneBlock = GameObject.FindWithTag("StartSceneBlock");
        if (startSceneBlock != null)
        {
            print("GameObject.FindWithTag = " + GameObject.FindWithTag("StartSceneBlock"));
            var initScript = startSceneBlock.GetComponent<StartSceneBlock>();
            yield return initScript.Initialize();
        }

        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}

//public enum SSS : string
//{
//    
//}