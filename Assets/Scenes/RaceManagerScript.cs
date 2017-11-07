using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManagerScript : MonoBehaviour {
   

   /*To do!
    Display 1st and 2nd(No animation in MK64)
    Display position based on tot distance (total lap length is the last item of triggerdist)
    It's possible we can replace the original positioning system with totdist
    Item fun times?
         */
    public static RaceManagerScript Singleton;



    public GameObject P1;
    public GameObject P2;
    public Vector2 lapCounts;
    public int P1LastCheckpoint=0;
    public int P2LastCheckpoint=0;
    public bool P1isFirst = true;
    public Transform[] triggers = new Transform[14];
    public float[] triggerDist = new float[14];
    public float P1TotDist = 0;
    public float P2TotDist = 0;
    public bool P1HasStarted = false;
    public bool P2HasStarted = false;
    //public Vector3[] triggerPos = new Vector3[14];
	void Start () {
        Singleton = this;
        //for(int i =0; i < triggers.Length; i++)
        //{
        // triggerPos[i] = triggers[i].transform.position;
        // }
        for (int i = 0; i < triggers.Length; i++)
        {
            if (i == 0)
            {
                triggerDist[i] = Vector3.Distance(triggers[i].position, triggers[i + 1].position);
            }
            else if (i == 13)
            {
                triggerDist[i] = triggerDist[i - 1] + Vector3.Distance(triggers[i].position, triggers[0].position);
            }
            else
            {
                triggerDist[i] = triggerDist[i - 1] + Vector3.Distance(triggers[i].position, triggers[i + 1].position);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (lapCounts.x > lapCounts.y)
        {
            P1isFirst = true;
        }else if (lapCounts.x < lapCounts.y)
        {
            P1isFirst = false;
        }else if (P1LastCheckpoint > P2LastCheckpoint)
        {
            P1isFirst = true;

        }
        else if (P1LastCheckpoint < P2LastCheckpoint)
        {
            P1isFirst = false;

        }else if(Vector3.Distance(P1.transform.position, triggers[P1LastCheckpoint].position)< Vector3.Distance(P2.transform.position, triggers[P1LastCheckpoint].position))
        {
            P1isFirst = true;
            
        }
        else if (Vector3.Distance(P1.transform.position, triggers[P1LastCheckpoint].position) > Vector3.Distance(P2.transform.position, triggers[P1LastCheckpoint].position))
        {
            P1isFirst = false;
        }else
        {
            P1isFirst = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("P1Pos= " + P1.transform.position);
            Debug.Log("P2Pos= " + P2.transform.position);
            Debug.Log("P1Distance: " + Vector3.Distance(P1.transform.position, triggers[P1LastCheckpoint].position));
            Debug.Log("P2Distance: " + Vector3.Distance(P2.transform.position, triggers[P2LastCheckpoint].position));
            Debug.Log("For reference, we think the next checkpoint is: " + triggers[P1LastCheckpoint]);
        }

        //For minimap
        if (P1HasStarted)
        {
            if (P1LastCheckpoint != 0)
            {
                P1TotDist = (lapCounts.x * triggerDist[triggerDist.Length - 1]) + triggerDist[P1LastCheckpoint - 1] - Vector3.Distance(P1.transform.position, triggers[P1LastCheckpoint].position);
            }
            else
            {
                P1TotDist = (lapCounts.x * triggerDist[triggerDist.Length - 1]) + triggerDist[triggerDist.Length - 1] - Vector3.Distance(P1.transform.position, triggers[P1LastCheckpoint].position);
            }
        }
        if (P2HasStarted)
        {
            if (P2LastCheckpoint != 0)
            {
                P2TotDist = (lapCounts.y * triggerDist[triggerDist.Length - 1]) + triggerDist[P2LastCheckpoint - 1] - Vector3.Distance(P2.transform.position, triggers[P2LastCheckpoint].position);
            }
            else
            {
                P2TotDist = (lapCounts.y * triggerDist[triggerDist.Length - 1]) + triggerDist[triggerDist.Length - 1] - Vector3.Distance(P2.transform.position, triggers[P2LastCheckpoint].position);
            }
        }
    }
}
