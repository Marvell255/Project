using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollect
{
    void Collect();
}

public class Star : MonoBehaviour, ICollect
{
    private HeroPortraitController _heroController;

    private const float MagnetRadius = 16f;
    private const float MagnetForce = 2.5f;

    private void Start()
    {
        _heroController = HeroPortraitController.Instance();
    }

    public void Collect()
    {
        Destroy(gameObject);

        PointsPanel.Instance().AddPoints(100);

        _heroController.PlayStarCollect();

    }

    private void FixedUpdate()
    {
        if (_heroController.MagnetActive)
        {
            var dis = Vector3.Distance(_heroController.transform.position, transform.position);

            if (dis < MagnetRadius)
            {
                var speed = MagnetRadius - dis;
                speed = speed * Time.deltaTime * MagnetForce;
                transform.position = Vector3.MoveTowards(transform.position, _heroController.transform.position, speed);
            }
        }
    }
}