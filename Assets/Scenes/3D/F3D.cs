using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class F3D : MonoBehaviour
{
    public AnimationCurve Curve;

    private void Update()
    {
//		transform.position += Vector3.right * Curve.Evaluate(Time.time);
    }


    private void OnCollisionEnter(Collision other)
    {
//		print(other.gameObject.name);
        if (other.gameObject.name == "ThirdPersonController")
            GetComponent<Animator>().Play("Jump");
    }
}