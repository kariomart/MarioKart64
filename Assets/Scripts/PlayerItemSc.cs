using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSc : MonoBehaviour {

    PlayerMovement MovementSc;
    public GameObject Banana;
    public GameObject BananaOne;
    public GameObject BananaTwo;
    public GameObject BananaThree;
    int TrioCount;
    public bool hasItem;
    public bool canBananaSingle;
    public bool canBananaTrio;
    public int playerID = 0;
    GameObject singleBanana;
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
            if (canBananaSingle == true)
            {
                singleBanana = Instantiate(Banana, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-.2f, gameObject.transform.position.z+.8f), Quaternion.identity, gameObject.transform);
            }
            if (canBananaTrio == true && TrioCount == 0)
            {
                BananaOne = Instantiate(Banana, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .2f, gameObject.transform.position.z + .8f), Quaternion.identity, gameObject.transform);
                BananaTwo = Instantiate(Banana, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .2f, gameObject.transform.position.z + 1f), Quaternion.identity, gameObject.transform);
                BananaThree = Instantiate(Banana, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .2f, gameObject.transform.position.z + 1.2f), Quaternion.identity, gameObject.transform);
                
            }

        }
        else if (hasItem == true && Input.GetButtonUp("FireP" + playerID))
        {
            if (canBananaSingle == true)
            {
                singleBanana.transform.parent = null;
                canBananaSingle = false;
                hasItem = false;
            }
            if (canBananaTrio == true)
            {
                if (TrioCount == 0)
                {
                    TrioCount = 3;
                }
                else if (TrioCount == 3)
                {
                    BananaThree.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 2)
                {
                    BananaTwo.transform.parent = null;
                    TrioCount--;
                }
                else if (TrioCount == 1)
                {
                    BananaOne.transform.parent = null;
                    TrioCount = 0;
                    canBananaTrio = false;
                    hasItem = false;
                }
                
            }
        }
    }

    public void equipItem(items currentItem)
    {
        Debug.Log("Current Item: " + currentItem);
        hasItem = true;
        if (currentItem == items.banana)
        {
            canBananaSingle = true;
        }

    }
    public void mushroomBoost()
    {
        //MovementSc.Boost();
    }
}
