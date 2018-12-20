using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public BoxCollider2D World;
    public float FollowSpeed = 0.125f;

    private Vector3 _smoothedPosition;
    private Camera _camera;

    private float _leftBound;
    private float _rightBound;
    private float _bottomBound;
    private float _topBound;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        GetBounds();

        _camera.transform.localPosition = new Vector3(Target.position.x, Target.position.y, _camera.transform.localPosition.z);
    }

    private void GetBounds()
    {
        var levelBounds = World.bounds;
        var camExtentV = _camera.orthographicSize;
        var camExtentH = camExtentV * Screen.width / Screen.height;

        _leftBound = levelBounds.min.x + camExtentH;
        _rightBound = levelBounds.max.x - camExtentH;
        _bottomBound = levelBounds.min.y + camExtentV;
        _topBound = levelBounds.max.y - camExtentV;
    }

    private void FixedUpdate()
    {
        _smoothedPosition = Vector3.Lerp(_camera.transform.localPosition, Target.localPosition, FollowSpeed);

        _smoothedPosition.x = Mathf.Clamp(_smoothedPosition.x, _leftBound, _rightBound);
        _smoothedPosition.y = Mathf.Clamp(_smoothedPosition.y, _bottomBound, _topBound);

        _camera.transform.localPosition = new Vector3(_smoothedPosition.x, _smoothedPosition.y, _camera.transform.localPosition.z);
    }
}