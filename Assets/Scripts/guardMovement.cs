using UnityEngine;
using System.Collections;

public class guardMovement : MonoBehaviour {

	public float speed = 6.0f;
	GameObject playerObject;
	UnityEngine.AI.NavMeshAgent agent;

	void Start () {
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	

	void Update () {
		persuePlayer ();
	}

	void persuePlayer() {
		agent.destination = playerObject.transform.position;
	}
}
