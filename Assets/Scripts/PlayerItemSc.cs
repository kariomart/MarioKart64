using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSc : MonoBehaviour {

    PlayerMovement MovementSc;
    public GameObject Banana;
    public GameObject GreenShell;
    public GameObject BadCube;
    GameObject singleBanana;
    GameObject Trio1;
    GameObject Trio2;
    GameObject Trio3;
    GameObject SingleShell;
    GameObject SingleBadCube;
    int TrioCount;
    public int shellSpeed = 30;
    public bool hasItem;
    public items currentItem;
    public int playerID = 0;
    Vector3 localForward;
    public float boostValue;
    public bool canBoost;
    // Use this for initialization
    void Start () {
        MovementSc = transform.GetComponent<PlayerMovement>();
        hasItem = false;
        TrioCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasItem == true && Input.GetButtonDown("FireP"+playerID))
        {
            if (currentItem == items.banana)
            {
                singleBanana = Instantiate(Banana, transform.position + (transform.forward * .8f), Quaternion.identity, gameObject.transform);
            }
            else if (currentItem == items.bananaBunch && TrioCount == 0)
            {
                if (Trio1 != null)
                {
                    Trio1 = null;
                    Trio2 = null;
                    Trio3 = null;
                }

                Trio1 = Instantiate(Banana, transform.position - (transform.forward * .8f), Quaternion.identity, gameObject.transform);
                Trio2 = Instantiate(Banana, transform.position - (transform.forward * 1), Quaternion.identity, gameObject.transform);
                Trio3 = Instantiate(Banana, transform.position - (transform.forward * 1.2f), Quaternion.identity, gameObject.transform);
            }
            else if (currentItem == items.greenShell)
            {
                SingleShell = null;

                SingleShell = Instantiate(GreenShell, transform.position + (transform.forward * 2), gameObject.transform.rotation, gameObject.transform);
                SingleShell.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (currentItem == items.greenShellTrio && TrioCount == 0)
            {
                if (Trio1 != null)
                {
                    Trio1 = null;
                    Trio2 = null;
                    Trio3 = null;
                }
                
                Trio1 = Instantiate(GreenShell, transform.position + (transform.forward * 1.5f), Quaternion.identity, gameObject.transform);
                Trio2 = Instantiate(GreenShell, transform.position + (transform.right * 1.5f), Quaternion.identity, gameObject.transform);
                Trio3 = Instantiate(GreenShell, transform.position + (transform.right * -1.5f), Quaternion.identity, gameObject.transform);
                Trio1.GetComponent<shellScript>().isTrio = true;
                Trio2.GetComponent<shellScript>().isTrio = true;
                Trio3.GetComponent<shellScript>().isTrio = true;
            }
            else if (currentItem == items.redShell)
            {

            }
            else if (currentItem == items.redShellTrio)
            {

            }
            else if (currentItem == items.badCube)
            {
                SingleBadCube = Instantiate(BadCube, transform.position + (transform.forward * .8f), Quaternion.identity, gameObject.transform);
            }
            else if (currentItem == items.mushroom)
            {
                MovementSc.Boost(1.5f,1);
                hasItem = false;
            }
            else if (currentItem == items.mushroomGolden)
            {
                StartCoroutine(GoldenTimer());
                MovementSc.Boost(1.5f, 1);
                hasItem = false;
            }
            
        }
        /*else if (canBoost && Input.GetButtonDown("FireP" + playerID))
        {
            MovementSc.Boost(1.5f, 1);
            Debug.Log("canboost");
        }*/
        else if (hasItem == true && Input.GetButtonUp("FireP" + playerID))
        {
            if (currentItem == items.banana)
            {
                singleBanana.transform.parent = null;
                hasItem = false;
            }
            else if (currentItem == items.bananaBunch)
            {
                if (TrioCount == 0)
                {
                    TrioCount = 3;
                }
                else if (TrioCount == 3)
                {
                    Trio3.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    Trio2.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    Trio1.transform.parent = null;
                    TrioCount = 0;
                    hasItem = false;
                }
                
            }
            else if (currentItem == items.greenShell)
            {
                SingleShell.transform.parent = null;
                SingleShell.GetComponent<Rigidbody>().isKinematic = false;
                SingleShell.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward*shellSpeed;
                hasItem = false;
            }
            else if (currentItem == items.greenShellTrio)
            {
                if (TrioCount == 0)
                {
                    TrioCount = 3;
                }
                else if (TrioCount == 3)
                {
                    Trio3.GetComponent<shellScript>().isTrio = false;
                    Trio3.transform.position = transform.position + (transform.forward * 2f);
                    Trio3.transform.parent = null;
                    Trio3.GetComponent<Rigidbody>().isKinematic = false;
                    Trio3.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward * shellSpeed;
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    Trio2.GetComponent<shellScript>().isTrio = false;
                    Trio2.transform.position = transform.position + (transform.forward * 2f);
                    Trio2.transform.parent = null;
                    Trio2.GetComponent<Rigidbody>().isKinematic = false;
                    Trio2.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward * shellSpeed;
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    Trio1.GetComponent<shellScript>().isTrio = false;
                    Trio1.transform.position = transform.position + (transform.forward * 2f);
                    Trio1.transform.parent = null;
                    Trio1.GetComponent<Rigidbody>().isKinematic = false;
                    Trio1.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward * shellSpeed;
                    TrioCount = 0;
                    hasItem = false;
                }
            }
            else if (currentItem == items.badCube)
            {
                SingleBadCube.transform.parent = null;
                hasItem = false;
            }
        }
    }

    public void equipItem(items toEquip)
    {
        Debug.Log("Current Item: " + toEquip);
        currentItem = toEquip;
        hasItem = true;
    }
    public IEnumerator GoldenTimer()
    {
        canBoost = true;
        yield return new WaitForSeconds(3);
        canBoost = false;
    }

}
