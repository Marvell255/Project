using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class SomeScript : MonoBehaviour
{
    [Inject] private GameController gameController;
    [Inject] private InjectClass injectClass;

    [UsedImplicitly]
    public void DoSomething()
    {
        print(gameController.Data + "   " + injectClass.Value);
    }
}