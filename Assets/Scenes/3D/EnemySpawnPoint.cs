using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject Enemy;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", Random.Range(1,6), Random.Range(2,3));
    }

    private void SpawnEnemy()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.2f);
    }
}