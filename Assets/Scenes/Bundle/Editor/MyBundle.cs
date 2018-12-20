using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private const string Path = @"C:\Users\pc\Documents\GitHub\Project\Assets\Scenes\Bundle\Files";

    [MenuItem("Assets/Create Bundle")]
    private static void CreateBundle()
    {
        var builds = new AssetBundleBuild[1];
        builds[0] = new AssetBundleBuild
        {
            assetBundleName = "item".ToLower(),
            assetNames = new[]
            {
                "Assets/Scenes/Bundle/NewHero.prefab",
                "Assets/AnyPortrait/Assets/Shaders/apShader_Transparent.shader"
            }
        };

        BuildPipeline.BuildAssetBundles(Path, builds, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }
}