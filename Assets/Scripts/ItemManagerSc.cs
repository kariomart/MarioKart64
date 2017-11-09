using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerSc : MonoBehaviour { //handles boxes, UI, which item the player gets

    public enum items
    {
        greenShell,
        greenShellTrio,
        redShell,
        redShellTrio,
        blueShell,
        banana,
        bananaBunch,
        mushroom,
        mushroomTrio,
        mushroomGolden,
        boo,
        badCube,
        invulnStar,
        lightning
    }
    int playerPos;
    public GameObject ItemBox;
    float boxTimer = 0;
    bool startTimer = false;
    public float boxRegen;
    bool canGrabItem;
    int itemNum;
    public items assignedItem;
        
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
        int posAtHit = playerPos;
        Debug.Log("BoxRegen start!");
        canGrabItem = false;
        startTimer = true;
        itemNum = Random.Range(0, 100);
        if (posAtHit == 1)
        {
            if (itemNum >= 0 && itemNum < 25)
            {
                //A item: green shell
                assignedItem = items.greenShell;
            }
            else if (itemNum >= 25 && itemNum < 40)
            {
                //B item: banana
                assignedItem = items.banana;
            }
            else if (itemNum >= 40 && itemNum < 50)
            {
                //mushroom
                assignedItem = items.mushroom;
            }
            else if (itemNum >= 50 && itemNum < 60)
            {
                //boo
                assignedItem = items.boo;
            }
            else if (itemNum >= 50 && itemNum < 60)
            {
                //upside down q mark cube
                assignedItem = items.badCube;
            }
            else if (itemNum >= 60 && itemNum < 70)
            {
                //green shell trio
                assignedItem = items.greenShellTrio;
            }
            else if (itemNum >= 70 && itemNum < 80)
            {
                //red shell
                assignedItem = items.redShell;
            }
            else if (itemNum >= 90 && itemNum <= 100)
            {
                //banana bunch
                assignedItem = items.bananaBunch;
            }
        }
        if (posAtHit == 2)
        {
            if (itemNum >=0 && itemNum < 15)
            {
                //b item: mushroom trio
                assignedItem = items.mushroomTrio;
            }
            else if (itemNum >= 15 && itemNum < 30)
            {
                //b item: golden mushroom
                assignedItem = items.mushroomGolden;
            }
            else if (itemNum >= 30 && itemNum < 45)
            {
                //b item: Red shell trio
                assignedItem = items.redShellTrio;
            }
            else if (itemNum >= 45 && itemNum < 60)
            {
                //b item: invincibility star
                assignedItem = items.invulnStar;
            }
            else if (itemNum >= 60 && itemNum < 68)
            {
                //C item: banana bunch
                assignedItem = items.bananaBunch;
            }
            else if (itemNum >= 68 && itemNum < 76)
            {
                //C item: lightning
                assignedItem = items.lightning;
            }
            else if (itemNum >= 76 && itemNum < 84)
            {
                //C item: blue shell
                assignedItem = items.blueShell;
            }
            else if (itemNum >= 84 && itemNum < 92)
            {
                //C item: red shell
                assignedItem = items.redShell;
            }
            else if (itemNum >= 92 && itemNum <= 100)
            {
                //C item: green shell trio
                assignedItem = items.greenShellTrio;
            }
        }
        
        yield return new WaitUntil(() => boxTimer >= boxRegen);
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        startTimer = false;
        boxTimer = 0;
        canGrabItem = true;
    }
}
