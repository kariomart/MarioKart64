using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapElementPositioning : MonoBehaviour {

    public int PlayerID = 0; //Manual for now, could be done by manager
    //bool StarActive = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<RectTransform>().position = new Vector3((RaceManagerScript.Singleton.TotDistances[PlayerID] / (RaceManagerScript.Singleton.triggerDist[RaceManagerScript.Singleton.triggerDist.Length - 1] * 3)) * Screen.width, Screen.height/2, 0);
        //Our Position is only changing in terms of x. Our position is the tot distance covered by a player divided by the length of the course (The percent complete) times the size of the screen
        if (PlayerID == 0&&RaceManagerScript.Singleton.P1isFirst)
        {

            this.transform.SetSiblingIndex(1);

        }else if (PlayerID == 0)
        {
            this.transform.SetSiblingIndex(0);
        }
    }

    
}
