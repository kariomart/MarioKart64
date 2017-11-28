using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float lerpSpeed;
    public float rotLerpSpeed = 0.1f;
	Vector3 pos;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform target = player.transform;
        
        transform.position = Vector3.Lerp(transform.position, target.position - target.forward * 2.5f + Vector3.up * .7f, lerpSpeed * Time.deltaTime);//This feels about perfect, and because of the lerping the player can get a bit ahead and replicate the camera zoom on stop. I am super proud of this one! -Clair

        
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(target.forward.x, 0, target.forward.z), rotLerpSpeed);//Martin's cool rotational lerp, going to play with it for drifting

	}
}
