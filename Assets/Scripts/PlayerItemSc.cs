using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSc : MonoBehaviour {

    PlayerMovement MovementSc;
    public GameObject Banana;
    GameObject singleBanana;
    GameObject Banana1;
    GameObject Banana2;
    GameObject Banana3;
    public GameObject GreenShell;
    GameObject SingleShell;
    GameObject GShell1;
    GameObject GShell2;
    GameObject GShell3;
    int TrioCount;
    public int shellSpeed = 500;
    public bool hasItem;
    public items currentItem;
    public int playerID = 0;
    Vector3 localForward;
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
                Banana1 = Instantiate(Banana, transform.position - (transform.forward * .8f), Quaternion.identity, gameObject.transform);
                Banana2 = Instantiate(Banana, transform.position - (transform.forward * 1), Quaternion.identity, gameObject.transform);
                Banana3 = Instantiate(Banana, transform.position - (transform.forward * 1.2f), Quaternion.identity, gameObject.transform);
            }
            else if (currentItem == items.greenShell)
            {
                SingleShell = Instantiate(GreenShell, transform.position + (transform.forward * 1), gameObject.transform.rotation, gameObject.transform);
                SingleShell.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (currentItem == items.greenShellTrio && TrioCount == 0)
            {
                GShell1 = Instantiate(GreenShell, transform.position + (transform.forward * 1.5f), Quaternion.identity, gameObject.transform);
                GShell2 = Instantiate(GreenShell, transform.position + (transform.right * 1.5f), Quaternion.identity, gameObject.transform);
                GShell3 = Instantiate(GreenShell, transform.position + (transform.right * -1.5f), Quaternion.identity, gameObject.transform);
                GShell1.GetComponent<ShellScript>().isTrio = true;
                GShell2.GetComponent<ShellScript>().isTrio = true;
                GShell3.GetComponent<ShellScript>().isTrio = true;
            }
            else if (currentItem == items.redShell)
            {

            }
        }
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
                    Banana3.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    Banana2.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    Banana1.transform.parent = null;
                    TrioCount = 0;
                    hasItem = false;
                }
                
            }
            else if (currentItem == items.greenShell)
            {
                SingleShell.transform.parent = null;
                SingleShell.GetComponent<Rigidbody>().isKinematic = false;
                SingleShell.GetComponent<Rigidbody>().AddForce(SingleShell.transform.forward*shellSpeed);
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
                    GShell3.GetComponent<ShellScript>().isTrio = false;
                    GShell3.transform.position = transform.position + (transform.forward * 1.2f);
                    GShell3.transform.parent = null;
                    GShell3.GetComponent<Rigidbody>().isKinematic = false;
                    GShell3.GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed);
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    GShell2.GetComponent<ShellScript>().isTrio = false;
                    GShell2.transform.position = transform.position + (transform.forward * 1.2f);
                    GShell2.transform.parent = null;
                    GShell2.GetComponent<Rigidbody>().isKinematic = false;
                    GShell2.GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed);
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    GShell1.GetComponent<ShellScript>().isTrio = false;
                    GShell1.transform.position = transform.position + (transform.forward * 1.2f);
                    GShell1.transform.parent = null;
                    GShell1.GetComponent<Rigidbody>().isKinematic = false;
                    GShell1.GetComponent<Rigidbody>().AddForce(transform.forward * shellSpeed);
                    TrioCount = 0;
                    hasItem = false;
                }
            }
        }
    }

    public void equipItem(items toEquip)
    {
        Debug.Log("Current Item: " + toEquip);
        currentItem = toEquip;
        hasItem = true;
    }
    public void mushroomBoost()
    {
        //MovementSc.Boost();
    }
}
