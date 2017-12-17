

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {


  //items stuff
  	public bool isInvincible;

	//Vector2 vel =  new Vector2(0, 0);
	Rigidbody rigid;
	public float speed=0;
	public float acceleration=.1f;
	public float maxSpeed=10;

    public Text raceTimer; // On the UI, the character's race time
    public Text lapCounter; // Lap counter
    bool isRaceOver=false;

	public float[] lapTimesMario; // Mario's Lap Times
	public float[] lapTimesLuigi; // Luigi's Lap Times
  
	Vector3 inputVector;
	//Clair's Variables
	public int playerId = 0;//PlayerID is Currently assigned in the inspector. We could also easily have it be assigned by RaceManager
    public bool CanGo = false;//Is this a silly way to handle this? Yes. Does it work in very few lines of code? Also yes. Set to true in RaceManager after 3 2 1.
    float boostDuration;
    float boostMultiplier;
    public bool[] drifting = { false, false };//If Drifting, False = left True = Right
    public bool hopping = false;
    float maxReverse = -3f;
   // public Transform kartModel;

    public Vector3 upDir;//For the normal based rotation - Clair
    public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;//-0.65, - , .452
    public Transform frontRight;
    public RaycastHit fl;
    public RaycastHit fr;
    public RaycastHit bl;
    public RaycastHit br;

    //Angular Velocity var -Clair
    public float angularCap=.5f;


    //Borrowing this name convention from the original implementation, we really should fix this -Clair
    float x;//Turning, between -1 and 1
    float y;//acceleration
    float z;//Triggers



    public GameObject PlayerCamera;

	// Use this for initialization
	void Start () {

		rigid = GetComponent<Rigidbody> ();
        lapTimesMario = new float[3]; // Mario's Lap Times
        lapTimesLuigi = new float[3]; // Luigi's Lap Times
                                       //kartModel = transform.Find("KartFrame");
    }

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Flip());
            //hopping = false;
        }
		if (CanGo) {//Someone correct me if I am wrong here, but putting this here instead of around translate is optimal I believe. When False, we skip this whole thing, rather than doing a needless part, and a bool check is a bool check - Clair
                    //this.transform.rotation = Quaternion.Euler (0, this.transform.rotation.eulerAngles.y, 0);


        // this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);//THIS WORKS but has weird repurcussions-Clair
            //Physics.gravity = new Vector3(0, -10f, 0);

            if (rigid.angularVelocity.x > angularCap)
            {
                rigid.angularVelocity = new Vector3(angularCap, rigid.angularVelocity.y, rigid.angularVelocity.z);
            }
            else if (rigid.angularVelocity.x <-1*angularCap)
            {
                rigid.angularVelocity = new Vector3(-1*angularCap, rigid.angularVelocity.y, rigid.angularVelocity.z);
            }

            if (rigid.angularVelocity.z > angularCap)
            {
                rigid.angularVelocity = new Vector3(rigid.angularVelocity.x, rigid.angularVelocity.y, angularCap);
            }
            else if (rigid.angularVelocity.z < -1 * angularCap)
            {
                rigid.angularVelocity = new Vector3(rigid.angularVelocity.x, rigid.angularVelocity.y, -1 * angularCap);
            }

            if (this.transform.rotation.eulerAngles.x > 40&& this.transform.rotation.eulerAngles.x < 100)//checking our rotation doesn't go crazy - Clair
            {
                Debug.Log("x>40");
                Debug.Log("rotation =" + this.transform.rotation.eulerAngles);

                this.transform.eulerAngles = new Vector3(40, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
                
            }
            else if (this.transform.rotation.eulerAngles.x < 320&& this.transform.rotation.eulerAngles.x > 260)
            {
                Debug.Log("x<-40");
                Debug.Log("rotation =" + this.transform.rotation.eulerAngles);
                this.transform.eulerAngles = new Vector3(320, this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);
            }

            if (this.transform.rotation.eulerAngles.z > 40 && this.transform.rotation.eulerAngles.z < 100)//checking our rotation doesn't go crazy - Clair
            {
                Debug.Log("z>40");
                Debug.Log("rotation =" + this.transform.rotation.eulerAngles);
                this.transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 40);


            }
            else if (this.transform.rotation.eulerAngles.z < 320 && this.transform.rotation.eulerAngles.z > 260)
            {
                Debug.Log("x<-40");
                Debug.Log("rotation =" + this.transform.rotation.eulerAngles);
                this.transform.eulerAngles = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 320);
               
            }



            /*Vector3 temp= new Vector3(0,0,0);
            int hits=0;
            if(Physics.Raycast(frontLeft.position, -1*this.transform.up, out fl))
            {
                if (fl.transform.tag == "Ground") {
                    hits++;
                    temp += fl.normal;
                }
                //Debug.Log(fl.normal);
            }

            if (Physics.Raycast(frontRight.position, -1 * this.transform.up, out fr))
            {
                if (fr.transform.tag == "ground")
                {
                    hits++;
                    temp += fr.normal;
                }
                //Debug.Log(fr.normal);
            }
            if (Physics.Raycast(backRight.position, -1 * this.transform.up, out br))
            {
                if (br.transform.tag == "ground")
                {
                    hits++;
                    temp += br.normal;
                }
                // Debug.Log(br.normal);
            }
            if (Physics.Raycast(backRight.position, -1 * this.transform.up, out bl))
            {
                if (bl.transform.tag == "ground")
                {
                    hits++;
                    temp += bl.normal;
                }
                // Debug.Log(bl.normal);
            }
            if (hits > 0)
            {
                temp = temp / hits;
                this.transform.rotation = Quaternion.FromToRotation(Vector3.up, temp);
            }
            else
            {
                Debug.Log("error!!!!");
            }*/
            //temp = (fl.normal + fr.normal + bl.normal + br.normal)/4;
            //Debug.Log(temp);
            //this.transform.up = new Vector3(temp.x, temp.y, temp.z);



            //We needed to be able to handle many unique players, therefore our Axis names must be dynamic. -Clair

            x = Input.GetAxis ("HorizontalP" + (playerId + 1)) * Time.deltaTime * 150.0f;

			//This one too - Clair
			y = Input.GetAxis ("AccelP" + (playerId + 1));

			z = Input.GetAxis ("TriggersP" + (playerId + 1));

            if (!drifting[0])
            {
                transform.Rotate(0, x / 2, 0);
            }
            else
            {
                //kartModel.rotation = Quaternion.Slerp(kartModel.rotation, Quaternion.Euler(new Vector3(0, x * 30, 0)),1*Time.deltaTime);
               // kartModel.forward = Vector3.Lerp(transform.forward, new Vector3((x*30)+this.transform.rotation.x, 0,0 ), 4f);
                if (!drifting[1])
                {
                    transform.Rotate(0, (x - .55f) / 2, 0);
                }
                else
                {
                    transform.Rotate(0, (x + .55f) / 2, 0);
                }
            }

            if (boostDuration > 0) {//Boosting is > everything - Clair
				speed = maxSpeed * boostMultiplier;//When boosting the speed is static, regardless of terrain slow down or player input. Using a multiplier allows us to give mini boosts, which is important for drifting and might be for the start sequence
				boostDuration -= Time.deltaTime;//Boosts last a certain amount of time...
				if (boostDuration <= 0) {//And when they run out, we return to the normal max speed
					speed = maxSpeed;
				}
			} else if (y > .05f) {

				if (speed < maxSpeed) {
					speed += (acceleration * y);
					//Debug.Log (speed);
				}
				if (speed > maxSpeed) {//Double purpose here- It would be easy to have speed go over the max with our accel (if you are at .99, max speed is 1, you could end up with a scenario where acceleration *y puts you over the max), and to prevent boost speed from continuing after the boost is over. - Clair
					speed = maxSpeed;
				}

			} else if (y < -.05) {//To distinguish between player slow down and no input - Clair

				if (speed > .01f) {//If we are holding back but moving forward.... -Clair

					speed *= 0.8f; // TODO: if this is a tuning value, I'd expose it as a specific var?

				} else if (speed <= .01f && speed >= maxReverse) {
					speed += acceleration * y;//Acceleration is positive, y is negative
					//speed = maxReverse;
				}
				if (speed > maxSpeed) {//If boostspeed is 1.5x max speed, boostspeed *.8 is still faster than max speed, meaning it would be optimal to decel after a boost without this check. -Clair
					speed = maxSpeed;
				}
			} else {//If no buttons are being hit... - Clair
				if (speed > 0) {

					speed *= 0.9f; //We slow down 

				} else if (speed < 0) {
					speed += acceleration;
				}
				if (speed > maxSpeed) {//If boostspeed is 1.5x max speed, boostspeed *.9 is still faster than max speed, meaning it would be optimal to decel after a boost without this check. -Clair
					speed = maxSpeed;
				}
			}


			//Drifting code below -Clair

			if (z > .05f && !hopping && !drifting [0]) {

				rigid.AddForce (new Vector3 (0, 3, 0), ForceMode.VelocityChange);
				hopping = true;

			} else if (z <= .05f) {
				drifting [0] = false;
			}





			rigid.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);


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
				int lapCount = RaceManagerScript.Singleton.lapCounts[playerId];

				if (lapCount == 0) {
					if (playerId == 0 /*Mario*/) {
						lapTimesMario [0] = Time.time; // Mario's Lap Times

					} else {
						lapTimesLuigi [0] = Time.time; // Luigi's Lap Times
					}
				} else if( lapCount <= 2){
					if (playerId == 0 /*Mario*/) {
						lapTimesMario [lapCount] = Time.time - lapTimesMario[lapCount-1]; // Mario's Lap Times
					} else {
						lapTimesLuigi [lapCount] = Time.time - lapTimesLuigi[lapCount-1]; // Luigi's Lap Times
					}
				}

				if (lapCount == 2 && playerId == 0) {
					//isRaceOver = true; 
					for (int i = 0; i < lapTimesMario.Length; i++) {
						string minutes = Mathf.Floor (lapTimesMario [i] / 60).ToString ("00");
						string seconds = (lapTimesMario [i] % 60).ToString ("00");

						raceTimer.text += "\n" + minutes + ":" + seconds;
					}
					string totalMinutes = Mathf.Floor (Time.time / 60).ToString ("00");
					string totalSeconds = (Time.time % 60).ToString ("00");

					raceTimer.text += "";
					raceTimer.text += "\nTotal:" + totalMinutes + ":" + totalSeconds;
				} else if(lapCount == 2 && playerId == 1) {
					//isRaceOver = true; 
					for (int i = 0; i < lapTimesLuigi.Length; i++) {
						string minutes = Mathf.Floor (lapTimesLuigi [i] / 60).ToString ("00");
						string seconds = (lapTimesLuigi [i] % 60).ToString ("00");

						raceTimer.text += "\n" + minutes + ":" + seconds;
					}
					string totalMinutes = Mathf.Floor (Time.time / 60).ToString ("00");
					string totalSeconds = (Time.time % 60).ToString ("00");

					raceTimer.text = "";
					raceTimer.text += "\nTotal:" + totalMinutes + ":" + totalSeconds;
				}
				
				RaceManagerScript.Singleton.lapCounts[playerId]++;
				lapCount = Mathf.Clamp (lapCount+2, 0, 3);
				if (playerId == 0) {
					lapCounter.text = "Lap: " + lapCount + "/3";
				}
                
     
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

        


	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            speed = .5f;
            acceleration = 0f;
            StartCoroutine(Flip());
            if (collision.gameObject != null)
            {
                Destroy(collision.gameObject);
            }
            
        }
        if (collision.gameObject.tag == "BananaTag" && !isInvincible)
        {
            //Debug.Log("Collided w banan");
            speed = .5f;
            acceleration = 0f;
            StartCoroutine(HitBanana());

            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.tag == "ground")//Clair's enter drift code
        {
            hopping = false;
            if (z > .1)//If we are still holding a trigger after the hop
            {
                drifting[0] = true;//We are drifting
                if (x > .1)//If we are turning right
                {
                    drifting[1] = true;//We are drifting right
                }
                else if (x < -.1)//Left edition
                {
                    drifting[1] = false;
                }
                else//But if the values are low then it was probably accidental, and the axis is returning to 0
                {
                    drifting[0] = false;//So we are not drifting
                }
            }
        }
    }



    
  public IEnumerator HitBanana()
    {
        float duration = 1;
        Quaternion StartRotation = transform.rotation;
        float t = 0f;
        PlayerCamera.GetComponent<CameraController>().cameraLock = true;
        while (t<duration)
        {
            transform.rotation = StartRotation * Quaternion.AngleAxis(t / duration * 720f, Vector3.up);
            yield return null;
            t += Time.deltaTime;
        }
        transform.rotation = StartRotation;
        //Debug.Log("HitBanana() activated");
        yield return new WaitForSeconds(1);
        PlayerCamera.GetComponent<CameraController>().cameraLock = false;
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