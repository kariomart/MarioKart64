using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSc : MonoBehaviour {

    PlayerMovement MovementSc;
    bool hasItem;
    public int playerID = 0;
    // Use this for initialization
    void Start () {
        MovementSc = transform.GetComponent<PlayerMovement>();
        hasItem = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hasItem == true && Input.GetButtonDown("FireP"+playerID))
        {

        }
	}

    public void equipItem(items currentItem)
    {
        Debug.Log("Current Item: " + currentItem);
        hasItem = true;

    }
    public void mushroomBoost()
    {
        //MovementSc.Boost();
    }
}
