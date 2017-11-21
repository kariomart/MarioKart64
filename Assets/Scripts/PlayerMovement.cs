using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  
  //items stuff
  public bool isInvincible;

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
		//transform.Translate (0, 0, 1 * speed * Time.deltaTime);
		rigid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
	}

	void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Flip());
        }



	}
	//Clair's Stuff
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "trigger1")// If we hit the starting line...
		{
			if (!RaceManagerScript.Singleton.HasStarted[playerId])//Either that player just started....
			{
				RaceManagerScript.Singleton.HasStarted[playerId] = true;
			}else if (RaceManagerScript.Singleton.LastCheckpoints[playerId] == 0)//Or that player's lap counter goes up if they have hit all the previous triggers
			{
				RaceManagerScript.Singleton.lapCounts[playerId]++;
			}
		}

       
    if (other.gameObject.name == "trigger" + (RaceManagerScript.Singleton.LastCheckpoints[playerId] + 1) && RaceManagerScript.Singleton.LastCheckpoints[playerId]< RaceManagerScript.Singleton.triggers.Length - 1)//If we've hit the next trigger and won't go out of bounds of the array...
    {
        RaceManagerScript.Singleton.LastCheckpoints[playerId]++;//We have hit the next trigger, and can't cheese, this is all cheese prevention
    }
    if (RaceManagerScript.Singleton.LastCheckpoints[playerId] == RaceManagerScript.Singleton.triggers.Length-1 && other.gameObject.name == "trigger" + RaceManagerScript.Singleton.triggers.Length)//If we have hit the last trigger before the starting line
    {
        RaceManagerScript.Singleton.LastCheckpoints[playerId] = 0;//Prepare the counter so we can go up a lap
    }
    if (other.gameObject.tag == "BananaTag" && !isInvincible)
        {
            //Debug.Log("Collided w banan");
            speed = .5f;
            acceleration = 0f;
            StartCoroutine(HitBanana());
            
            Destroy(other.gameObject);
            
        }
    if (other.gameObject.tag == "Shell")
        {
            speed = .5f;
            acceleration = 0f;
            StartCoroutine(Flip());
            Destroy(other.gameObject);
        }
	}
    
  IEnumerator HitBanana()
    {
        float duration = 1;
        Quaternion StartRotation = transform.rotation;
        float t = 0f;
        while (t<duration)
        {
            transform.rotation = StartRotation * Quaternion.AngleAxis(t / duration * 720f, Vector3.up);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = StartRotation;
        //Debug.Log("HitBanana() activated");
        yield return new WaitForSeconds(1);
        acceleration = 0.1f;
    }

    IEnumerator Flip()
    {
        float duration = 1;
        Quaternion StartRotation = transform.rotation;
        float t = 0f;
        rigid.constraints = RigidbodyConstraints.None; ;
        rigid.AddForce(transform.up * 100);
        while (t < duration)
        {
            transform.rotation = StartRotation * Quaternion.AngleAxis(t / duration * 720f, Vector3.up);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = StartRotation;
        yield return new WaitForSeconds(1);
        acceleration = 0.1f;
    }
}


