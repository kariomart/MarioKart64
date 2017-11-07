using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Vector2 vel =  new Vector2(0, 0);
	Rigidbody rigid;
	public float speed;
	public float acceleration;
	public float maxSpeed;

	bool left;
	bool right;
	bool space; 


	float power;

	Vector3 inputVector;



	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update () {


		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		transform.Rotate(0, x, 0);



		if (Input.GetKey (KeyCode.Space)) {

			if (speed < maxSpeed) {
				speed += acceleration;
				Debug.Log (speed);
			}

		} else {

			if (speed > 0) {

				speed *= 0.8f;

			}
		}


		transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}

	void FixedUpdate() {




	}
}
