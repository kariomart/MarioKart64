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

    public RectTransform FirstMarker;
    public RectTransform SecondMarker;
    //public Vector2 lapCounts;
    public int[] lapCounts = { 0, 0 };
    public int[] LastCheckpoints = { 0, 0 };
    public bool P1isFirst = true;
    public Transform[] triggers = new Transform[14];
    public float[] triggerDist = new float[14];

    public float[] TotDistances = { 0, 0 };
   
    public bool[] HasStarted = { false, false };

    
    //public Vector3[] triggerPos = new Vector3[14];
	void Start () {
        Singleton = this;

        
	// TODO: WHAT IS THIS DOING??? TELL US
        for (int i = 0; i < triggers.Length; i++)//The distances in this array are CUMULATIVE. 
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
        if (TotDistances[0] < TotDistances[1])// Whoever has covered more ground is first
        {
            P1isFirst = false;
            SecondMarker.position = new Vector3(Screen.width*.1f, Screen.height*.65f,0);
            FirstMarker.position = new Vector3(Screen.width * .1f, Screen.height * .1f, 0);

        }
        else
        {
            P1isFirst = true;
            FirstMarker.position = new Vector3(Screen.width * .1f, Screen.height * .65f, 0);
            SecondMarker.position = new Vector3(Screen.width * .1f, Screen.height * .1f, 0);
        }
        
        if (HasStarted[0])
        {
            if (LastCheckpoints[0]!= 0)//If P1 has started
            {
                //their total distance equals their laps completed * course length + the distance from the start of the course to their next checkpoint - the distance between them and their next checkpoint
                TotDistances[0] = (lapCounts[0] * triggerDist[triggerDist.Length - 1]) + triggerDist[LastCheckpoints[0]-1] - Vector3.Distance(P1.transform.position, triggers[LastCheckpoints[0]].position);
                
            }
            else
            {//                                                                         This is the difference! LastCheckpoint equalling 0 means we are ready to go up a lap counter, so we want to compare to the total track distance
                TotDistances[0] = (lapCounts[0] * triggerDist[triggerDist.Length - 1]) + triggerDist[triggerDist.Length - 1] - Vector3.Distance(P1.transform.position, triggers[LastCheckpoints[0]].position);
            }
        }
        if (HasStarted[1])//P2 Version of above
        {
            if (LastCheckpoints[1] != 0)
            {
                TotDistances[1] = (lapCounts[1] * triggerDist[triggerDist.Length - 1]) + triggerDist[LastCheckpoints[1] - 1] - Vector3.Distance(P2.transform.position, triggers[LastCheckpoints[1]].position);
            }
            else
            {
                TotDistances[1] = (lapCounts[1] * triggerDist[triggerDist.Length - 1]) + triggerDist[triggerDist.Length - 1] - Vector3.Distance(P2.transform.position, triggers[LastCheckpoints[1]].position);
            }
        }
    }
}
