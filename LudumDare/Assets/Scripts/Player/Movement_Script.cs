using UnityEngine;
using System.Collections;

public class Movement_Script : MonoBehaviour {
	public float speed;
	public float gravity;
	public float jumpSpeed;
	float fallSpeed;
	float initialJumpSpeed;
	bool jumped;
	GameObject floor;
	int coll;
	bool inAir;

	// Use this for initialization
	void Start () {
		jumped = false;
		initialJumpSpeed = jumpSpeed;
		floor = GameObject.FindGameObjectWithTag ("Floor");
		coll = 1;
		fallSpeed = 0;
		inAir = false;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("Horizontal");
		if (transform.position.x > -26.40545 && horizontal < 0 || transform.position.x < 28.61565 && horizontal > 0) {
			transform.position += Vector3.right * speed * horizontal * Time.deltaTime;
		}

		if(Input.GetButton("Jump") && !jumped) {
			jumped = true;
			inAir = true;
		}

		if (jumped) {
			jump ();
		}

		if (inAir) {
			transform.position += Vector3.down * fallSpeed * Time.deltaTime;
			fallSpeed += gravity;
		}

		if (!inAir) {
			fallSpeed = 0;
			if (jumped && jumpSpeed < 0) {
				jumped = false;
				jumpSpeed = initialJumpSpeed;
			}
		}
	}

	void jump() {
		transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
		jumpSpeed -= gravity;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.CompareTag ("Floor") || col.gameObject.CompareTag ("Platform")) {
			floor = col.gameObject;
			inAir = false;
		}
		print ("collision" + coll);
		coll++;
	}
}
