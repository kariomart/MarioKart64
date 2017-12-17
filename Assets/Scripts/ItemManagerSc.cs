
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
public class ItemManagerSc : MonoBehaviour { //handles boxes, UI, which item the player gets

   
    int playerPos;
    public GameObject ItemBox;
    public GameObject QMark;
    float boxTimer = 0;
    bool startTimer = false;
    public float boxRegen;
    int itemNum;
    int playerID;
    public items assignedItem;
    public PlayerItemSc PlayerSc;
    public PlayerMovement PlayerMovementSc;
    public float xRot;
    public float yRot;
    public float zRot;
    public GameObject P1ItemText;
    public GameObject P2ItemText;
    public Image P1ItemImage;
    public Image P2ItemImage;
    public Sprite[] ItemImages;
    public BoxCollider boxCollider;
   

    // Use this for initialization
    void Start () {
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Instantiate(QMark, gameObject.transform.position, Quaternion.identity, gameObject.transform);//It might be better to have the itembox instantiate this, or have the itembox be the parent and set world rotation as needed -Clair
        P1ItemText = GameObject.Find("P1ItemText");
        P2ItemText = GameObject.Find("P2ItemText");
        P1ItemImage = GameObject.Find("P1ItemImage").GetComponent<Image>();
        P2ItemImage = GameObject.Find("P2ItemImage").GetComponent<Image>();
        xRot = Random.Range(-.4f, .4f);
        if (xRot <= 0)
        {
            yRot = 1;
        }
        else if (xRot >= 0.001f)
        {
            yRot = -1;
        }
        zRot = Random.Range(0.5f, .8f);
        boxCollider = transform.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		if (startTimer)
        {
            boxTimer += Time.deltaTime;
        }
        transform.Rotate(xRot,yRot,zRot);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerSc = other.GetComponent<PlayerItemSc>();
            PlayerMovementSc = other.GetComponent<PlayerMovement>();
            playerID = PlayerMovementSc.playerId;
            if (PlayerSc.canGrabItem == true)
            {
                
                Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);
                StartCoroutine(BoxRegen());
                //item delivery goes here
            }
        }
    }

    IEnumerator BoxRegen()
    {
        PlayerSc.canGrabItem = false;
        boxCollider.enabled = false;
        startTimer = true;
        itemNum = Random.Range(0, 100);
        Debug.Log(itemNum);
        if ((playerID == 0 && RaceManagerScript.Singleton.P1isFirst) || (playerID == 1 && !RaceManagerScript.Singleton.P1isFirst))
        {
            Debug.Log("player1");
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
                //assignedItem = items.boo;
                assignedItem = items.banana;
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
                //assignedItem = items.redShell;
                assignedItem = items.greenShell;
            }
            else if (itemNum >= 90 && itemNum <= 100)
            {
                //banana bunch
                assignedItem = items.bananaBunch;
            }
        }
        if ((playerID == 1 && RaceManagerScript.Singleton.P1isFirst) || (playerID == 0 && !RaceManagerScript.Singleton.P1isFirst))
        {
            Debug.Log("player2");
            if (itemNum >=0 && itemNum < 15)
            {
                //b item: mushroom trio
                //assignedItem = items.mushroomTrio;
                assignedItem = items.mushroom;
            }
            else if (itemNum >= 15 && itemNum < 30)
            {
                //b item: golden mushroom
                //assignedItem = items.mushroomGolden;
                assignedItem = items.mushroom;
            }
            else if (itemNum >= 30 && itemNum < 45)
            {
                //b item: Red shell trio
                assignedItem = items.redShellTrio;
            }
            else if (itemNum >= 45 && itemNum < 60)
            {
                //b item: invincibility star
                //assignedItem = items.invulnStar;
                assignedItem = items.mushroom;
            }
            else if (itemNum >= 60 && itemNum < 68)
            {
                //C item: banana bunch
                assignedItem = items.bananaBunch;
            }
            else if (itemNum >= 68 && itemNum < 76)
            {
                //C item: lightning
                //assignedItem = items.lightning;
                assignedItem = items.badCube;
            }
            else if (itemNum >= 76 && itemNum < 84)
            {
                //C item: blue shell
                //assignedItem = items.blueShell;
                assignedItem = items.greenShellTrio;
            }
            else if (itemNum >= 84 && itemNum < 92)
            {
                //C item: red shell
                //assignedItem = items.redShell;
                assignedItem = items.redShell;
            }
            else if (itemNum >= 92 && itemNum <= 100)
            {
                //C item: green shell trio
                assignedItem = items.greenShellTrio;
            }
        }
        //The following is an alternate item roulette for the items we currently have working
        /*if (itemNum >= 0 && itemNum < 25)
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
            //upside down q mark cube
            assignedItem = items.badCube;
        }
        else if (itemNum >= 60 && itemNum < 80)
        {
            //green shell trio
            assignedItem = items.greenShellTrio;
        }
        else if (itemNum >= 80 && itemNum <= 100)
        {
            //banana bunch
            assignedItem = items.bananaBunch;
        }*/
        Debug.Log("Roll: " + itemNum);
        Debug.Log("Assigned item: " + assignedItem);
        PlayerSc.equipItem(assignedItem);
        
        if (playerID == 0)
        {
            P1ItemText.GetComponent<Text>().text = "" + assignedItem;
            Debug.Log("Assigned Item num: " + (int)assignedItem);
            P1ItemImage.sprite = ItemImages[(int)assignedItem];
        }
        else if (playerID == 1)
        {
            P2ItemText.GetComponent<Text>().text = "" + assignedItem;
            Debug.Log("Assigned Item num: " + (int)assignedItem);
            P2ItemImage.sprite = ItemImages[(int)assignedItem];
        }
        xRot = Random.Range(0.1f, .4f);
        yRot = -yRot;
        zRot = Random.Range(0.5f, .8f);
        yield return new WaitUntil(() => boxTimer >= boxRegen);
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Instantiate(QMark, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        startTimer = false;
        boxCollider.enabled = true;
        boxTimer = 0;
    }
}