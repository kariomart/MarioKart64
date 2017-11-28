using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour {
    Rigidbody shellRB;
    Vector3 oldVelocity;
    public PlayerMovement PlayerMoveSc;
    public bool isTrio;
    public bool isLaunched;
    public Transform rotTarget;
    public float orbitDistance = 3.0f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 relativeDistance = Vector3.zero;
    bool isRed;
    bool isBlue;



    // Use this for initialization
    void Start() {
        shellRB = gameObject.GetComponent<Rigidbody>();
        shellRB.freezeRotation = true;
        isLaunched = true;
        rotTarget = transform.parent;
        if (rotTarget != null)
        {
            relativeDistance = transform.position - rotTarget.position;
        }
    }

    void FixedUpdate()
    {
        oldVelocity = shellRB.velocity;
    }
    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Shell")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if (collision.gameObject.tag != "Player")
        {
            int bounces = 0;
            if (bounces <= 5 && !isRed)
            {
                ContactPoint contact = collision.contacts[0];
                Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);
                shellRB.velocity = reflectedVelocity;
                Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
                transform.rotation = rotation * transform.rotation;
                //Debug.Log("Bounces: " + bounces);
                bounces += 1;
                Debug.Log("bounces: " + bounces);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        else
        {
            Destroy(gameObject);
            PlayerMoveSc = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }

    void Orbit()
    {
        if (rotTarget != null)
        {
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
}

