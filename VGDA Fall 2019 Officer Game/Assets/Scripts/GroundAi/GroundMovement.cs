using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GroundMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] waypoints;
    private void Start()
    {
        agent.SetDestination(waypoints[0].transform.position);
    }
    void Update()
    {
        if (Vector3.Distance(agent.destination, transform.position) <= 1f)
        {
            int target = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[target].transform.position);
        }

    }
}
