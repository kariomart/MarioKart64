using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemSc : MonoBehaviour {

    PlayerMovement MovementSc;
    public GameObject Banana;
    public GameObject GreenShell;
    public GameObject RedShell;
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
    public shellScript[] TrioSc;
    public Rigidbody[] TrioRB;
    public GameObject P1ItemText;
    public GameObject P2ItemText;
    public Image P1ItemImage;
    public Image P2ItemImage;
    public bool canGrabItem;
    public Sprite noItemSprite;

    // Use this for initialization
    void Start () {
        MovementSc = transform.GetComponent<PlayerMovement>();
        hasItem = false;
        TrioCount = 0;
        TrioSc = new shellScript[4];
        TrioRB = new Rigidbody[4];
        P1ItemText = GameObject.Find("P1ItemText");
        P2ItemText = GameObject.Find("P2ItemText");
        P1ItemImage = GameObject.Find("P1ItemImage").GetComponent<Image>();
        P2ItemImage = GameObject.Find("P2ItemImage").GetComponent<Image>();
        canGrabItem = true;
        //P1ItemImage.sprite = noItemSprite;
        //P2ItemImage.sprite = noItemSprite;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasItem == true && Input.GetButtonDown("FireP"+playerID))
        {
            if (currentItem == items.banana)
            {
                singleBanana = Instantiate(Banana, transform.position - (transform.forward * 1.9f) + transform.up * .2f, Quaternion.identity, gameObject.transform);
            }
            else if (currentItem == items.bananaBunch && TrioCount == 0)
            {
                if (Trio1 != null)
                {
                    Trio1 = null;
                    Trio2 = null;
                    Trio3 = null;
                }

                Trio1 = Instantiate(Banana, transform.position - (transform.forward * 1.9f) - transform.up*.01f, Quaternion.identity);
                Trio2 = Instantiate(Banana, transform.position - (transform.forward * 2.6f) - transform.up * .01f, Quaternion.identity);
                Trio3 = Instantiate(Banana, transform.position - (transform.forward * 3.3f) - transform.up * .01f, Quaternion.identity);

                Trio1.transform.parent = transform;
                Trio2.transform.parent = transform;
                Trio3.transform.parent = transform;

                TrioRB[1] = Trio1.GetComponent<Rigidbody>();
                TrioRB[2] = Trio2.GetComponent<Rigidbody>();
                TrioRB[3] = Trio3.GetComponent<Rigidbody>();

            }
            else if (currentItem == items.greenShell)
            {
                SingleShell = null;

                SingleShell = Instantiate(GreenShell, transform.position + (transform.forward * 2), gameObject.transform.rotation);
                SingleShell.transform.parent = transform;
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
                
                Trio1 = Instantiate(GreenShell, transform.position + (transform.forward * 2.1f) + transform.up*0.01f, gameObject.transform.rotation);
                Trio2 = Instantiate(GreenShell, transform.position + (Vector3.Normalize(transform.right *2.1f + transform.forward) * -2.1f) + transform.up * 0.01f, gameObject.transform.rotation);
                Trio3 = Instantiate(GreenShell, transform.position + (Vector3.Normalize(-transform.right*2.1f + transform.forward) * -2.1f) + transform.up * 0.01f, gameObject.transform.rotation);

                //(Quaternion.Euler(0,135,0)*Vector3.forward)
                /*Trio1.transform.parent = transform;
                Trio2.transform.parent = transform;
                Trio3.transform.parent = transform;*/
                TrioSc[1] = Trio1.GetComponent<shellScript>();
                TrioSc[2] = Trio2.GetComponent<shellScript>();
                TrioSc[3] = Trio3.GetComponent<shellScript>();

                TrioRB[1] = Trio1.GetComponent<Rigidbody>();
                TrioRB[2] = Trio2.GetComponent<Rigidbody>();
                TrioRB[3] = Trio3.GetComponent<Rigidbody>();

                TrioSc[1].rotTarget = transform;
                TrioSc[2].rotTarget = transform;
                TrioSc[3].rotTarget = transform;

                TrioRB[1].isKinematic = true;
                TrioRB[2].isKinematic = true;
                TrioRB[3].isKinematic = true;

                TrioSc[1].isTrio = true;
                TrioSc[2].isTrio = true;
                TrioSc[3].isTrio = true;

            }
            else if (currentItem == items.redShell)
            {
                SingleShell = null;

                SingleShell = Instantiate(RedShell, transform.position + (transform.forward * 2), gameObject.transform.rotation);
                SingleShell.transform.parent = transform;
                SingleShell.GetComponent<Rigidbody>().isKinematic = true;
                SingleShell.GetComponent<shellScript>().playerLaunched = playerID;
            }
            else if (currentItem == items.redShellTrio && TrioCount == 0)
            {
                if (Trio1 != null)
                {
                    Trio1 = null;
                    Trio2 = null;
                    Trio3 = null;
                }

                Trio1 = Instantiate(RedShell, transform.position + (transform.forward * 2.1f) + transform.up * 0.01f, gameObject.transform.rotation);
                Trio2 = Instantiate(RedShell, transform.position + (Vector3.Normalize(transform.right * 2.1f + transform.forward) * -2.1f) + transform.up * 0.01f, gameObject.transform.rotation);
                Trio3 = Instantiate(RedShell, transform.position + (Vector3.Normalize(-transform.right * 2.1f + transform.forward) * -2.1f) + transform.up * 0.01f, gameObject.transform.rotation);

                //(Quaternion.Euler(0,135,0)*Vector3.forward)
                /*Trio1.transform.parent = transform;
                Trio2.transform.parent = transform;
                Trio3.transform.parent = transform;*/
                TrioSc[1] = Trio1.GetComponent<shellScript>();
                TrioSc[2] = Trio2.GetComponent<shellScript>();
                TrioSc[3] = Trio3.GetComponent<shellScript>();

                TrioRB[1] = Trio1.GetComponent<Rigidbody>();
                TrioRB[2] = Trio2.GetComponent<Rigidbody>();
                TrioRB[3] = Trio3.GetComponent<Rigidbody>();

                TrioSc[1].rotTarget = transform;
                TrioSc[2].rotTarget = transform;
                TrioSc[3].rotTarget = transform;

                TrioRB[1].isKinematic = true;
                TrioRB[2].isKinematic = true;
                TrioRB[3].isKinematic = true;

                TrioSc[1].isTrio = true;
                TrioSc[2].isTrio = true;
                TrioSc[3].isTrio = true;

                TrioSc[1].playerLaunched = playerID;
                TrioSc[2].playerLaunched = playerID;
                TrioSc[3].playerLaunched = playerID;

            }
            else if (currentItem == items.badCube)
            {
                SingleBadCube = Instantiate(BadCube, transform.position + (transform.forward * -2.2f) + transform.up * .1f, Quaternion.identity);
                SingleBadCube.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                Physics.IgnoreCollision(SingleBadCube.GetComponent<Collider>(), GetComponent<Collider>());
                SingleBadCube.transform.parent = transform;

            }
            else if (currentItem == items.mushroom)
            {
                MovementSc.Boost(1.5f,1);
                canGrabItem = true;
                resetItemText();
                hasItem = false;
            }
            else if (currentItem == items.mushroomGolden)
            {
                StartCoroutine(GoldenTimer());
                MovementSc.Boost(1.5f, 1);
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
                canGrabItem = true;
                resetItemText();
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
                    StartCoroutine(BananaCollideTrue());
                    Trio3.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    StartCoroutine(BananaCollideTrue());
                    Trio2.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    StartCoroutine(BananaCollideTrue());
                    Trio1.transform.parent = null;
                    TrioCount = 0;
                    resetItemText();
                    canGrabItem = true;
                    hasItem = false;
                }
                
            }
            else if (currentItem == items.greenShell)
            {
                SingleShell.transform.parent = null;
                SingleShell.GetComponent<shellScript>().StartFreeStart();
                SingleShell.GetComponent<Rigidbody>().isKinematic = false;
                SingleShell.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward*shellSpeed;
                resetItemText();
                canGrabItem = true;
                hasItem = false;
            }
            else if (currentItem == items.redShell)
            {
                SingleShell.transform.parent = null;
                SingleShell.GetComponent<shellScript>().StartFreeStart();
                SingleShell.GetComponent<Rigidbody>().isKinematic = false;
                //SingleShell.GetComponent<Rigidbody>().velocity = SingleShell.transform.forward * shellSpeed;
                SingleShell.GetComponent<shellScript>().isRed = true;
                resetItemText();
                canGrabItem = true;
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
                    if (Trio3 != null)
                    {
                        TrioSc[3].isTrio = false;
                        TrioSc[3].rotTarget = null;
                        if (TrioSc[3] != null) { TrioSc[3].StartFreeStart(); }
                        Trio3.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[3].isKinematic = false;
                        TrioRB[3].velocity = transform.forward * shellSpeed;
                    }
                    
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    if (Trio2 != null)
                    {
                        TrioSc[2].isTrio = false;
                        TrioSc[2].rotTarget = null;
                        if (TrioSc[2] != null) { TrioSc[2].StartFreeStart(); }
                        Trio2.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[2].isKinematic = false;
                        TrioRB[2].velocity = transform.forward * shellSpeed;
                    }
                    
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    if (Trio1 != null)
                    {
                        TrioSc[1].isTrio = false;
                        TrioSc[1].rotTarget = null;
                        if (TrioSc[1] != null) { TrioSc[1].StartFreeStart(); }
                        Trio1.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[1].isKinematic = false;
                        TrioRB[1].velocity = transform.forward * shellSpeed;
                    }
                    
                    resetItemText();
                    canGrabItem = true;
                    hasItem = false;
                }
            }
            else if (currentItem == items.redShellTrio)
            {
                if (TrioCount == 0)
                {
                    TrioCount = 3;
                }
                else if (TrioCount == 3)
                {
                    if (Trio3 != null)
                    {
                        if (TrioSc[3] != null) { TrioSc[3].StartFreeStart(); }
                        TrioSc[3].isTrio = false;
                        TrioSc[3].rotTarget = null;
                        Trio3.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[3].isKinematic = false;
                        //TrioRB[3].velocity = transform.forward * shellSpeed;
                        TrioSc[3].isRed = true;
                    }
                    
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    if (Trio2 != null)
                    {
                        if (TrioSc[2] != null) { TrioSc[2].StartFreeStart(); }
                        TrioSc[2].isTrio = false;
                        TrioSc[2].rotTarget = null;
                        Trio2.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[2].isKinematic = false;
                        //TrioRB[2].velocity = transform.forward * shellSpeed;
                        TrioSc[2].isRed = true;
                    }
                    
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    if (Trio1 != null)
                    {
                        if (TrioSc[1] != null) { TrioSc[1].StartFreeStart(); }
                        TrioSc[1].isTrio = false;
                        TrioSc[1].rotTarget = null;
                        Trio1.transform.position = transform.position + (transform.forward * 2f);
                        TrioRB[1].isKinematic = false;
                        //TrioRB[1].velocity = transform.forward * shellSpeed;
                        TrioSc[1].isRed = true;
                    }
                    
                    resetItemText();
                    canGrabItem = true;
                    TrioCount = 0;
                    hasItem = false;
                }
            }
            else if (currentItem == items.badCube)
            {
                SingleBadCube.transform.parent = null;
                StartCoroutine(BadCubeCollideTrue());
                resetItemText();
                canGrabItem = true;
                hasItem = false;
            }
        }
    }
    public void resetItemText()
    {
        if (playerID == 0)
        {
            P1ItemText.GetComponent<Text>().text = "No Item";
            Debug.Log("Image: " + noItemSprite);
            P1ItemImage.sprite = noItemSprite;
        }
        else if (playerID == 1)
        {
            P2ItemText.GetComponent<Text>().text = "No Item";
            Debug.Log("Image: " + noItemSprite);

            P2ItemImage.sprite = noItemSprite;
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
    public IEnumerator BadCubeCollideTrue()
    {
        Physics.IgnoreCollision(SingleBadCube.GetComponent<Collider>(), GetComponent<Collider>(), true);
        yield return new WaitForSeconds(1);
        Physics.IgnoreCollision(SingleBadCube.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
    public IEnumerator BananaCollideTrue()
    {
        if (TrioRB[3] != null)
        {
            Physics.IgnoreCollision(Trio3.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        if (TrioRB[2] != null)
        {
            Physics.IgnoreCollision(Trio2.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        if (TrioRB[1] != null)
        {
            Physics.IgnoreCollision(Trio1.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        yield return new WaitForSeconds(1);
        if (TrioRB[3] != null)
        {
            Physics.IgnoreCollision(Trio3.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        if (TrioRB[2] != null)
        {
            Physics.IgnoreCollision(Trio2.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
        if (TrioRB[1] != null)
        {
            Physics.IgnoreCollision(Trio1.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
    }

}
