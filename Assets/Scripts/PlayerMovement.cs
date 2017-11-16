using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//Vector2 vel =  new Vector2(0, 0);
	Rigidbody rigid;
	public float speed=0;
	public float acceleration=.1f;
	public float maxSpeed=10;

	// TODO: if these are unused, you should say so, and/or remove them
	bool left;
	bool right;
	bool space; 

	// TODO: what is "power"? what does this do? it's not clear
	float power;

	Vector3 inputVector;
    //Clair's Variables
    public int playerId = 0;//PlayerID is Currently assigned in the inspector. We could also easily have it be assigned by RaceManager


	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (this.transform.rotation.eulerAngles.x > 30)//checking our rotation doesn't go crazy - Clair
        {
            this.transform.rotation = Quaternion.Euler(30, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);

        }else if(this.transform.rotation.eulerAngles.x < -30)
        {
            this.transform.rotation = Quaternion.Euler(-30, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
        }

        if (this.transform.rotation.eulerAngles.z > 30)//checking our rotation doesn't go crazy - Clair
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 30);
        }
        else if (this.transform.rotation.eulerAngles.z < -30)
        {
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, -30);
        }*/
            //Temporary lock so we can't end up upside down- Clair
            //A better way might be rounding down to ~30 or -30

            //We needed to be able to handle many unique players, therefore our Axis names must be dynamic. -Clair
            var x = Input.GetAxis("HorizontalP"+(playerId+1)) * Time.deltaTime * 150.0f;
		transform.Rotate(0, x, 0);

        //This one too - Clair
        var y = Input.GetAxis("AccelP" + (playerId + 1));
		
		// TODO: formatting here is really weird? in MonoDevelop, you can format the doc automatically if you want

        if (y>.05f) {

			if (speed < maxSpeed) {
				speed += (acceleration*y);
				//Debug.Log (speed);
			}

		} else if(y<-.05) {//To distinguish between player slow down and no input - Clair

			if (speed > 0) {//If we are holding back but moving forward.... -Clair

				speed *= 0.8f; // TODO: if this is a tuning value, I'd expose it as a specific var?

			}
        }
        else
        {	// TODO: what is this doing and why?
            if (speed > 0)
            {

                speed *= 0.9f; // TODO: if this is a tuning value, I'd expose it as a specific var?

            }
        }

		// TODO: is it intentional to not use CharacterController or Rigidbody here? are you sure you don't need collision?
		// TODO: is the road completely flat?
		transform.Translate (0, 0, 1 * speed * Time.deltaTime);
	}

	void FixedUpdate() {




	}
    //Clair's Stuff
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "trigger1")
        {
            if (!RaceManagerScript.Singleton.HasStarted[playerId])
            {
                RaceManagerScript.Singleton.HasStarted[playerId] = true;
            }else if (RaceManagerScript.Singleton.LastCheckpoints[playerId] == 0)
            {
                RaceManagerScript.Singleton.lapCounts[playerId]++;
            }
        }

        // TODO: it would be nice to comment this for other group members?
       // Debug.Log("hit: " + other.gameObject.name);
        if (other.gameObject.name == "trigger" + (RaceManagerScript.Singleton.LastCheckpoints[playerId] + 1) && RaceManagerScript.Singleton.LastCheckpoints[playerId]< 13)
        {
            RaceManagerScript.Singleton.LastCheckpoints[playerId]++;
        }
	    // TODO: it would be nice to check the actual length of the array or item count, instead of hard coding a length of 13 or 14 etc
        if (RaceManagerScript.Singleton.LastCheckpoints[playerId] == 13 && other.gameObject.name == "trigger14")
        {
            RaceManagerScript.Singleton.LastCheckpoints[playerId] = 0;
        }
	    // TODO: again, just comment on what each chunk is doing?

        
    }
}
