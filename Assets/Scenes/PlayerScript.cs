using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
            transform.rotation = Quaternion.identity;
            Vector3 toMove = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                toMove += new Vector3(0, 0, .1f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                toMove += new Vector3(.1f, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                toMove -= new Vector3(0, 0, .1f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                toMove -= new Vector3(.1f, 0, 0);
            }
            this.transform.position += (toMove.normalized / 5f);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit: " + other.gameObject.name);
        if (other.gameObject.name == "trigger" + (RaceManagerScript.Singleton.P1LastCheckpoint+1)&& RaceManagerScript.Singleton.P1LastCheckpoint <13)
        {
            RaceManagerScript.Singleton.P1LastCheckpoint++;
        }
        if (RaceManagerScript.Singleton.P1LastCheckpoint == 13 && other.gameObject.name == "trigger14")
        {
            RaceManagerScript.Singleton.P1LastCheckpoint = 0;
        }
        if (other.gameObject.name == "trigger1")
        {
            if (!RaceManagerScript.Singleton.P1HasStarted)
            {
                RaceManagerScript.Singleton.P1HasStarted = true;
            }
            else
            {
                RaceManagerScript.Singleton.lapCounts.x++;
            }
        }
    }
}
