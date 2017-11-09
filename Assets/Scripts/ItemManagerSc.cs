using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerSc : MonoBehaviour { //handles boxes, UI, which item the player gets

    int playerPos;
    public GameObject ItemBox;
    float boxTimer = 0;
    bool startTimer = false;
    public float boxRegen;
    bool canGrabItem;
    int itemNum;
	// Use this for initialization
	void Start () {
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        canGrabItem = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (startTimer)
        {
            boxTimer += Time.deltaTime;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide!");
        if (canGrabItem == true)
        {
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(BoxRegen());
            //item delivery goes here
        }
    }

    IEnumerator BoxRegen()
    {
        Debug.Log("BoxRegen start!");
        canGrabItem = false;
        startTimer = true;
        itemNum = Random.Range(0, 100);
        if (playerPos == 1)
        {
            if (itemNum >= 0 && itemNum < 25)
            {
                //A item
            }
            else if (itemNum >= 25 && itemNum < 40)
            {
                //B item
            }
            else if (itemNum >= 40 && itemNum < 50)
            {
                //C item 1
            }
            else if (itemNum >= 50 && itemNum < 60)
            {

            }
            else if (itemNum >= 50 && itemNum < 60)
            {

            }
            else if (itemNum >= 60 && itemNum < 70)
            {

            }
            else if (itemNum >= 70 && itemNum < 80)
            {

            }
            else if (itemNum >= 90 && itemNum <= 100)
            {

            }
        }
        
        yield return new WaitUntil(() => boxTimer >= boxRegen);
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        startTimer = false;
        boxTimer = 0;
        canGrabItem = true;
    }
}
