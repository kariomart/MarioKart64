using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTwoScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.identity;
        Vector3 toMove = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            toMove += new Vector3(0, 0, .1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            toMove += new Vector3(.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            toMove -= new Vector3(0, 0, .1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            toMove -= new Vector3(.1f, 0, 0);
        }
        this.transform.position += (toMove.normalized / 5f);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit: " + other.gameObject.name);
        if (other.gameObject.name == "trigger" + (RaceManagerScript.Singleton.P2LastCheckpoint + 1) && RaceManagerScript.Singleton.P2LastCheckpoint <13)
        {
            RaceManagerScript.Singleton.P2LastCheckpoint++;
        }
        if (RaceManagerScript.Singleton.P2LastCheckpoint == 13 && other.gameObject.name == "trigger14")
        {
            RaceManagerScript.Singleton.P2LastCheckpoint = 0;
            
        }
        if (other.gameObject.name == "trigger1")
        {

            if (!RaceManagerScript.Singleton.P2HasStarted)
            {
                RaceManagerScript.Singleton.P2HasStarted = true;
            }
            else
            {
                RaceManagerScript.Singleton.lapCounts.y++;
            }
        }

    }
}
