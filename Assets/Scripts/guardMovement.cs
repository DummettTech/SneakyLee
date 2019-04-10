using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class guardMovement : MonoBehaviour {

    float persueSpeed;
    GameObject playerObject;
    GameObject[] waypoints;
    NavMeshAgent agent;
    bool foundPlayer = false;
    public string route = "";

    int currentWaypoint = 0;

    void Start () {

        string routeName = "Waypoint" + route;
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
        waypoints = GameObject.FindGameObjectsWithTag(routeName);

        persueSpeed = agent.speed + 2.0f;
	}
	
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            NavMeshHit hit;
            agent.Raycast(playerObject.transform.position, out hit);
            if (!hit.hit)
            {
                foundPlayer = true;
            }
        }
    }

    
	void Update () {

        if (foundPlayer)
        {
            persuePlayer();
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

        agent.destination = waypoints[currentWaypoint].transform.position;
        currentWaypoint = currentWaypoint + 1;
    }

	void persuePlayer() {
        agent.speed = persueSpeed;

        // depending on how it plays, might want to change speed when persuing
		agent.destination = playerObject.transform.position;
	}


}
