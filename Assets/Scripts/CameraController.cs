﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float lerpSpeed;
    public float rotLerpSpeed = 0.1f;
	Vector3 pos;
	public GameObject player;
    public bool cameraLock;
    PlayerMovement playerScript;
	// Use this for initialization
	void Start () {
        cameraLock = false;
        playerScript = player.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log("Cameralock = " + cameraLock);
        if (!cameraLock)
        {
            Transform target = player.transform;

            //transform.position = Vector3.Lerp(transform.position, target.position - target.forward * 6.75f + Vector3.up * 1.5f, lerpSpeed * Time.deltaTime);//This feels about perfect, and because of the lerping the player can get a bit ahead and replicate the camera zoom on stop. I am super proud of this one! -Clair
            //transform.position = Vector3.Lerp(transform.position, target.position - target.forward * 6.75f + Vector3.up * 1.5f, lerpSpeed * Time.deltaTime);//This feels about perfect, and because of the lerping the player can get a bit ahead and replicate the camera zoom on stop. I am super proud of this one! -Clair
            transform.position = Vector3.Lerp(transform.position, target.position - target.forward * 3.25f + Vector3.up * 1.5f, lerpSpeed * Time.deltaTime);//This feels about perfect, and because of the lerping the player can get a bit ahead and replicate the camera zoom on stop. I am super proud of this one! -Clair

            if (playerScript.drifting[0])
            {
                transform.forward = Vector3.Lerp(transform.forward, new Vector3(target.forward.x, 0, target.forward.z), rotLerpSpeed*.5f);//Martin's cool rotational lerp, going to play with it for drifting
            }else
            {
                transform.forward = Vector3.Lerp(transform.forward, new Vector3(target.forward.x, 0, target.forward.z), rotLerpSpeed);//Martin's cool rotational lerp, going to play with it for drifting

            }
        }
	}	
}
