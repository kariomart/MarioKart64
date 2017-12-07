using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour {
    Rigidbody shellRB;
    Vector3 oldVelocity;
    public PlayerMovement PlayerMoveSc;
    public bool isTrio;
    public bool isLaunched;
    public bool finishedLaunch;
    public Transform rotTarget;
    public float orbitDistance = 3.0f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 relativeDistance = Vector3.zero;
    public bool isRed;
    bool isBlue;
    public int playerLaunched;
    int playerToTrack;
    public GameObject Mario;
    public GameObject Luigi;
    public Transform shellTarget;
    public float redShellSpeed;
    int bounces;


    // Use this for initialization
    void Start() {
        shellRB = gameObject.GetComponent<Rigidbody>();
        shellRB.freezeRotation = true;
        isLaunched = true;
        finishedLaunch = false;
        if (rotTarget != null)
        {
            relativeDistance = transform.position - rotTarget.position;
        }
        bounces = 0;
        Mario = GameObject.Find("Mario");
        Luigi = GameObject.Find("Luigi");
        if (playerLaunched == 0)
        {
            playerToTrack = 1;
            shellTarget = Luigi.transform;
        }
        if (playerLaunched == 1)
        {
            playerToTrack = 0;
            shellTarget = Mario.transform;
        }
        Debug.Log("Launched: " + playerLaunched + " Target: " + shellTarget);
    }

    void FixedUpdate()
    {
        oldVelocity = shellRB.velocity;
    }
    // Update is called once per frame
    void Update() {

        //Avoid collision with ground code
        Ray castDown = new Ray(transform.position, Vector3.down);
        float maxRayDistance = 1f;
        RaycastHit hitFloor;
        Debug.DrawRay(castDown.origin, castDown.direction * maxRayDistance, Color.yellow);
        if (Physics.Raycast(castDown, out hitFloor))
        {
           // Debug.Log("grounded check hit something");
            transform.position = new Vector3(transform.position.x, hitFloor.point.y+.5f,transform.position.z);
        }
        else
        {
            //Debug.Log("grounded check hit nothing");
            
        }

        if (isRed && Vector3.Distance(transform.position,shellTarget.position)<30)
        {
            //tracking code goes here
            transform.position = Vector3.MoveTowards(transform.position,shellTarget.position,redShellSpeed*Time.deltaTime);
            Debug.Log("tracking");
        }
        else if (isRed)
        {
            shellRB.velocity = transform.forward * 25;
            Debug.Log("not tracking");
        }
        Debug.Log("shell pos: " + transform.position + " target pos " + shellTarget.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            if (finishedLaunch == false)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
            if (finishedLaunch == true)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>(), false);

                Destroy(collision.gameObject);
                Destroy(gameObject);

            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (finishedLaunch == false && collision.transform != transform)
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
            else if (finishedLaunch == true)
            {
                collision.gameObject.GetComponent<PlayerMovement>().Flip();
                Destroy(gameObject);
            }

        }
        else
        {
            if (bounces <= 5 && !isRed)
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);
                shellRB.velocity = reflectedVelocity;
                Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;
                //Debug.Log("Bounces: " + bounces);
                bounces += 1;
                //Debug.Log("bounces: " + bounces);
            }
            else if (!isRed)
            {
                Destroy(gameObject);
            }

        }
  
    }

    void Orbit()
    {
        if (rotTarget != null)
        {
            Debug.Log("Rotating!");
            transform.position = rotTarget.position + relativeDistance;
            transform.RotateAround(rotTarget.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
            // Reset relative position after rotate
            relativeDistance = transform.position - rotTarget.position;
        }
    }
    private void LateUpdate()
    {
        if (isTrio)
        {
            Orbit();
        }
    }
    public void StartFreeStart()
    {
        StartCoroutine(FreeStart());
    }
    public IEnumerator FreeStart() //disables collisions with player and rotating shells immediately after launch
    {
        yield return new WaitForSeconds(.3f);
        finishedLaunch = false;
    }
}

