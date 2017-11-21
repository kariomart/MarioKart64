using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float lerpSpeed;
	Vector3 pos;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform target = player.transform;

		transform.position = target.position - target.forward * 5 + Vector3.up * 1;
//		transform.rotation = Quaternion.Slerp (transform.rotation, player.transform.rotation, Time.deltaTime);

		//transform.LookAt (new Vector3(target.position.x, transform.position.y, target.position.z), Vector3.up);
		transform.forward = Vector3.Lerp(transform.forward, new Vector3(target.forward.x, 0, target.forward.z), lerpSpeed);

	}
}
