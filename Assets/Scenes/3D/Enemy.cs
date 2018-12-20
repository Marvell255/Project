using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    public float Step = 8;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        GoToNextDestination();
    }

    private void GoToNextDestination()
    {
        StartCoroutine(RandomizeDestination());
    }

    private IEnumerator RandomizeDestination()
    {
        _navMeshAgent.destination =
            transform.position + new Vector3(Random.Range(-Step, Step), 0, Random.Range(-Step, Step));

        yield return new WaitForSeconds(2f);

        Invoke("GoToNextDestination", Random.Range(.3f, 3));
    }

    private void OnParticleCollision(GameObject other)
    {
//        print(other.name);
        Destroy(gameObject);
    }
}