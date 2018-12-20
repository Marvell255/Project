using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject GrenadePrefab;
    public ProgressBar ProgressBar;
    public Vector3 Offset;

    private void Update()
    {
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            ThrowGrenadeObject(ProgressBar.ForceEnd());
        }

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            ProgressBar.ForceStart();
        }
    }

    private void ThrowGrenadeObject(float force)
    {
//        print(force);
        var grenade = Instantiate(GrenadePrefab, Camera.main.transform.position + Camera.main.transform.forward + Offset, Quaternion.identity, transform);
        grenade.transform.SetParent(null, true);
        var grenadeRigidbody = grenade.GetComponent<Rigidbody>();

        grenadeRigidbody.AddForce(Camera.main.transform.forward * 4 * (2 * force), ForceMode.Impulse);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Camera.main.transform.position + Camera.main.transform.forward + Offset, 0.2f);
    }
}