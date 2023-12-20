using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWapointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWapointIndex].transform.position, transform.position) < .1f)
        {
            currentWapointIndex++;
            if(currentWapointIndex >= waypoints.Length)
            {
                currentWapointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWapointIndex].transform.position, Time.deltaTime * speed);
    }
}
