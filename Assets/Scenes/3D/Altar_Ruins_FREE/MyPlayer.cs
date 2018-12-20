using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyPlayer : MonoBehaviour
{
    [Range(1, 20)] public float Speed = 2;
    public float Gravity;
    public float JumpForce = 2;

    public ParticleSystem ParticleSystem;

    

    private CharacterController Controller;
    private Vector3 MoveDirection;

    private Vector3 CameraRotation;
    private Transform CameraTransform;
    public float Sensitivity;


    public AudioSource AudioSource;
    public AudioClip AudioBackground;
    public AudioClip AudioShoot;
    public AudioClip AudioJump;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        CameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;

        AudioSource.PlayOneShot(AudioBackground);
    }

    private void Update()
    {
        Move();
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
//        Debug.DrawRay(CameraTransform.position, CameraTransform.forward * 99, Color.yellow);

        if (Input.GetMouseButtonDown(0))
        {
            ParticleSystem.Play();
            var particle = ParticleSystem.main;
            particle.loop = true;

            InvokeRepeating("Shake", 0f, .1f);
//            Shake();
//            var ray = new Ray(CameraTransform.position, CameraTransform.forward * 99);
//            RaycastHit raycastHit;
//            if (Physics.Raycast(ray, out raycastHit, 99))
//            {
//                print(raycastHit.transform.gameObject.name);
//                if (raycastHit.transform.gameObject.tag.Equals("Enemy"))
//                {
//                    Destroy(raycastHit.transform.gameObject);
//                }
//            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            var particle = ParticleSystem.main;
            particle.loop = false;
            CancelInvoke("Shake");
        }
    }

    private Vector3 _shakeVector = Vector3.zero;

    private void Shake()
    {
        var onUnitSphere = Random.onUnitSphere;
        onUnitSphere.y += 1.2f;
        _shakeVector += onUnitSphere;

        AudioSource.PlayOneShot(AudioShoot);
    }

    private void FixedUpdate()
    {
        _shakeVector = Vector3.Lerp(_shakeVector, Vector3.zero, .65f);
    }

    private void Move()
    {
        if (Controller.isGrounded)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            MoveDirection = new Vector3(h, 0, v);
            MoveDirection = transform.TransformDirection(MoveDirection);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveDirection.y = JumpForce;
                AudioSource.PlayOneShot(AudioJump);
            }
        }

        MoveDirection.y -= Gravity * Time.deltaTime;
        Controller.Move(MoveDirection * Speed * Time.deltaTime);
    }


    private void Rotate()
    {
        var delta = Sensitivity * Time.deltaTime;
        var x = Input.GetAxis("Mouse X") * delta;
        transform.Rotate(Vector3.up * x);
        var y = Input.GetAxis("Mouse Y") * delta;
        transform.Rotate(Vector3.up * x);

        CameraRotation.x -= y;
        CameraRotation.x = Mathf.Clamp(CameraRotation.x, -75, 50);
        CameraTransform.localEulerAngles = CameraRotation;
    }
}