using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

	public float speed = 5.0f;

	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody playerRigidbody;
    private Animator playerAnimator;

	private void Start () {
		playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
	}

    private void Update () {
		Move (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
	}

	void Move (float horizontal, float vertical) {
        bool isRunning = horizontal != 0 || vertical != 0;
        playerAnimator.SetBool("isRunning", isRunning);

        moveDirection.Set (horizontal, 0.0f, vertical);

		moveDirection = moveDirection.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (moveDirection + transform.position);

        if (horizontal != 0 || vertical != 0)
        {
            transform.forward = Vector3.Normalize(moveDirection);
        }
        
    }
}
