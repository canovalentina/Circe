using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavigation : MonoBehaviour
{
    public GameObject arrowContainer;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private float checkInThreshold = 4f;

    void Start()
    {
        arrowContainer.SetActive(waypoints.Length > 0);
    }

    void Update()
    {
        if (waypoints.Length > 0 && currentWaypointIndex < waypoints.Length)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            float distanceToWaypoint = Vector3.Distance(currentWaypoint.position, transform.position);
            if (currentWaypointIndex + 1 < waypoints.Length)
            {
                Transform nextWaypoint = waypoints[currentWaypointIndex + 1];
                float distanceToNextWaypoint = Vector3.Distance(nextWaypoint.position, transform.position);
                //if next waypoint is closer than current, it means we probably ran over current one
                // and might as well switch to next one
                if (distanceToNextWaypoint < distanceToWaypoint)
                {
                    currentWaypointIndex++;
                    return;
                }
                
            } 

            if (distanceToWaypoint < checkInThreshold)
            {
                //go to next waypoint
                currentWaypointIndex++;
            }
            else
            {
                Vector3 direction = currentWaypoint.position - transform.position;
                direction.y = 0f;
                Quaternion rotation = Quaternion.LookRotation(direction);
                arrowContainer.transform.rotation = rotation;
            } 
        } else
        {
            arrowContainer.SetActive(false);
        }
    }
}
