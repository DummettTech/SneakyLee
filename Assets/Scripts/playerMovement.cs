using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public float speed = 5.0f;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody playerRigidbody;

	void Start () {
		playerRigidbody = GetComponent<Rigidbody>();
	}
	

	void Update () {
		move (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
	}

	// TODO: Add gravity, verticle solutions to levels would be fun
	void move(float i, float j) {
		moveDirection.Set (i, 0.0f, j);
		//moveDirection = transform.TransformDirection (moveDirection);
		moveDirection = moveDirection.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (moveDirection + transform.position);

        if (i != 0 || j !=0)
        {
            transform.forward = Vector3.Normalize(moveDirection);
        }

    }
}
