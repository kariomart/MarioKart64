using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSc : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void equipItem(items currentItem)
    {
        Debug.Log("Current Item: " + currentItem);
    }
}
