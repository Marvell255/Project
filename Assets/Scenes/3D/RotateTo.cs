using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public GameObject Target;
    public bool Follow;

    private void LateUpdate()
    {
        if (!Follow) return;

        var heading = Target.transform.position + Vector3.up - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), 0.05f);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 36), "Toggle"))
        {
            Follow = !Follow;
        }

        if (GUI.Button(new Rect(10, 50, 150, 36), "Rotate"))
        {
            transform.eulerAngles += new Vector3(0, 10);
        }
    }
}