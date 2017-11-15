using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Vector2 vel =  new Vector2(0, 0);
	Rigidbody rigid;
	public float speed=0;
	public float acceleration=.1f;
	public float maxSpeed=10;

	bool left;
	bool right;
	bool space; 


	float power;

	Vector3 inputVector;
    //Clair's Variables
    public int playerId = 0;


	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody> ();
		acceleration = 0.1f;
		maxSpeed = 10f;
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);//Temporary lock so we can't end up upside down- Clair
        //A better way might be rounding down to ~30 or -30

        //We needed to be able to handle many unique players
		var x = Input.GetAxis("HorizontalP"+(playerId+1)) * Time.deltaTime * 150.0f;
		transform.Rotate(0, x, 0);

        //This too
        var y = Input.GetAxis("AccelP" + (playerId + 1));
        //Debug.Log(y);
		if (y>.05f) {

			if (speed < maxSpeed) {
				speed += acceleration;
				Debug.Log (speed);
			}

		} else if(y<-.05) {//To distinguish between player slow down and no input

			if (speed > 0) {

				//speed *= 0.8f;
				speed += -acceleration;

			}
        }
        else
        {
            if (speed > 0)
            {

                speed *= 0.9f;

            }
        }


		transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}

	void FixedUpdate() {




	}
    //Clair's Stuff
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit: " + other.gameObject.name);
        if (other.gameObject.name == "trigger" + (RaceManagerScript.Singleton.LastCheckpoints[playerId] + 1) && RaceManagerScript.Singleton.LastCheckpoints[playerId]< 13)
        {
            RaceManagerScript.Singleton.LastCheckpoints[playerId]++;
        }
        if (RaceManagerScript.Singleton.LastCheckpoints[playerId] == 13 && other.gameObject.name == "trigger14")
        {
            RaceManagerScript.Singleton.LastCheckpoints[playerId] = 0;
        }
        if (other.gameObject.name == "trigger1")
        {
            if (!RaceManagerScript.Singleton.HasStarted[playerId])
            {
                RaceManagerScript.Singleton.HasStarted[playerId] = true;
            }
            else
            {
                RaceManagerScript.Singleton.lapCounts[playerId]++;
            }
        }
    }
}

				speed += (acceleration*y);
				Debug.Log (speed);