using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 B = Vector3.zero;
    public float Speed = 0.5f;

    private Vector3 _startPoint;
    private Vector3 _endPoint;

    private float _timer;

    private void Awake()
    {
        GetPoints();
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        transform.position = Vector3.Lerp(_startPoint, _endPoint, Mathf.PingPong(_timer * Speed, 1));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0.43f);

        Gizmos.DrawSphere(_startPoint, 0.32f);
        Gizmos.DrawSphere(_endPoint, 0.32f);

        Gizmos.DrawLine(_startPoint, _endPoint);
    }

    private void GetPoints()
    {
        _startPoint = transform.position;
        _endPoint = transform.position + B;
    }

    private void OnValidate()
    {
        GetPoints();
    }
}