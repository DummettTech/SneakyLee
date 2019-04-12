using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class guardMovement : MonoBehaviour {

    public float fieldOfViewAngle = 140f;
    SphereCollider guardVisionBall;

    GameObject playerObject;
    GameObject[] waypoints;
    NavMeshAgent agent;
    bool foundPlayer = false;
    public string route = "";
    private Light guardLight;
    int currentWaypoint = 0;

    float pursueSpeed;
    void Start () {
        string routeName = "Waypoint" + route;

		playerObject = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
        waypoints = GameObject.FindGameObjectsWithTag(routeName);
        guardLight = GetComponentInChildren<Light>();
        guardVisionBall = GetComponent<SphereCollider>();
        pursueSpeed = agent.speed + 2.0f;
	}
	
    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Vector3 direction = col.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if(angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, guardVisionBall.radius);
                if (hit.collider.gameObject == playerObject)
                    {
                        foundPlayer = true;
                        guardLight.color = Color.red;
                    }
            }
        }
    }

	void Update () {

        if (foundPlayer)
        {
            pursuePlayer();
        } else
        {
            if(!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                patrol();
            }
        }
     
	}

    void patrol ()
    {
        if (currentWaypoint >= waypoints.Length) currentWaypoint = 0;
        float yCoord = waypoints[currentWaypoint].transform.localScale.y;
     
        if (yCoord != 1)
        {
            StartCoroutine(PauseFor(yCoord - 1));
            
        }
        agent.destination = waypoints[currentWaypoint].transform.position;
        currentWaypoint = currentWaypoint + 1;
    }

	void pursuePlayer() {
        agent.speed = pursueSpeed;
        // depending on how it plays, might want to change speed when persuing
		agent.destination = playerObject.transform.position;
	}

    IEnumerator PauseFor(float seconds)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(seconds);
        agent.isStopped = false;
    }

}
