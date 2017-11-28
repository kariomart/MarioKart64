using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

//HEY! HEY LISTEN! We need to multiply things by delta time guys, or there could be unintended behavior on low powered machines. - Clair  

  //items stuff
  public bool isInvincible;

	//Vector2 vel =  new Vector2(0, 0);
	Rigidbody rigid;
	public float speed=0;
	public float acceleration=.1f;
	public float maxSpeed=10;
    float maxReverse = -3f;

	// TODO: if these are unused, you should say so, and/or remove them
	bool left;
	bool right;
	bool space; 

	// TODO: what is "power"? what does this do? it's not clear
	float power;

	Vector3 inputVector;
	//Clair's Variables
	public int playerId = 0;//PlayerID is Currently assigned in the inspector. We could also easily have it be assigned by RaceManager
    public bool CanGo = false;//Is this a silly way to handle this? Yes. Does it work in very few lines of code? Also yes. Set to true in RaceManager after 3 2 1.
    float boostDuration;
    float boostMultiplier;
    public GameObject PlayerCamera;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () {
        if (CanGo)//Someone correct me if I am wrong here, but putting this here instead of around translate is optimal I believe. When False, we skip this whole thing, rather than doing a needless part, and a bool check is a bool check - Clair
        {
            this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.eulerAngles.y, 0);

            //We needed to be able to handle many unique players, therefore our Axis names must be dynamic. -Clair

            var x = Input.GetAxis("HorizontalP" + (playerId + 1)) * Time.deltaTime * 150.0f;
            transform.Rotate(0, x, 0);

            //This one too - Clair
            var y = Input.GetAxis("AccelP" + (playerId + 1));

            if (boostDuration > 0)//Boosting is > everything - Clair
            {
                speed = maxSpeed * boostMultiplier;//When boosting the speed is static, regardless of terrain slow down or player input. Using a multiplier allows us to give mini boosts, which is important for drifting and might be for the start sequence
                boostDuration -= Time.deltaTime;//Boosts last a certain amount of time...
                if (boostDuration <= 0)//And when they run out, we return to the normal max speed
                {
                    speed = maxSpeed;
                }
            }
            else if (y > .05f)
            {

                if (speed < maxSpeed)
                {
                    speed += (acceleration * y);
                    //Debug.Log (speed);
                }
                if (speed > maxSpeed)//Double purpose here- It would be easy to have speed go over the max with our accel (if you are at .99, max speed is 1, you could end up with a scenario where acceleration *y puts you over the max), and to prevent boost speed from continuing after the boost is over. - Clair
                {
                    speed = maxSpeed;
                }

            }
            else if (y < -.05)
            {//To distinguish between player slow down and no input - Clair

                if (speed > .01f)
                {//If we are holding back but moving forward.... -Clair

                    speed *= 0.8f; // TODO: if this is a tuning value, I'd expose it as a specific var?

                }
                else if (speed <= .01f && speed >= maxReverse)
                {
                    Debug.Log("Heyo");
                    speed += acceleration * y;//Acceleration is positive, y is negative
                    //speed = maxReverse;
                }
                if (speed > maxSpeed)//If boostspeed is 1.5x max speed, boostspeed *.8 is still faster than max speed, meaning it would be optimal to decel after a boost without this check. -Clair
                {
                    speed = maxSpeed;
                }
            }
            else//If no buttons are being hit... - Clair
            { 
                if (speed > 0)
                {

                    speed *= 0.9f; //We slow down 

                }else if (speed < 0)
                {
                    speed += acceleration;
                }
                if (speed > maxSpeed)//If boostspeed is 1.5x max speed, boostspeed *.9 is still faster than max speed, meaning it would be optimal to decel after a boost without this check. -Clair
                {
                    speed = maxSpeed;
                }
            }

            // TODO: is it intentional to not use CharacterController or Rigidbody here? are you sure you don't need collision?
            // TODO: is the road completely flat?
            //transform.Translate (0, 0, 1 * speed * Time.deltaTime);
            rigid.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }
	}

	void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Flip());
        }



	}
	//Clair's Stuff
    public void Boost(float multiplier, float duration)
    {
        boostMultiplier = multiplier;//Literally importing values
        boostDuration = duration;


    }
    
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

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "shell")
		{
			StartCoroutine(Flip());
		}
	}
    
  public IEnumerator HitBanana()
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

    public IEnumerator Flip()
    {
        float duration = 1;
        Quaternion StartRotation = transform.rotation;
        float t = 0f;
        //rigid.constraints = RigidbodyConstraints.FreezePositionZ;
        PlayerCamera.GetComponent<CameraController>().cameraLock = true;
        rigid.AddForce(transform.up * 300);
        acceleration = 0f;

        while (t < duration)
        {
            transform.rotation = StartRotation * Quaternion.AngleAxis(t / duration * 720f, Vector3.up);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = StartRotation;
        yield return new WaitForSeconds(1);
        PlayerCamera.GetComponent<CameraController>().cameraLock = false;
        yield return new WaitForSeconds(1);
        acceleration = 0.1f;

       // rigid.constraints = RigidbodyConstraints.None;

    }
}
