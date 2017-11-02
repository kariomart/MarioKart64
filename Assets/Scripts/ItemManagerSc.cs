using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerSc : MonoBehaviour {

    public GameObject ItemBox;
    float boxTimer = 0;
    bool startTimer = false;
    public float boxRegen;
    bool canGrabItem;
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
            Destroy(transform.GetChild(0));
            StartCoroutine(BoxRegen());
            //item delivery goes here
        }
    }

    IEnumerator BoxRegen()
    {
        Debug.Log("BoxRegen start!");
        canGrabItem = false;
        startTimer = true;
        yield return new WaitUntil(() => boxTimer >= boxRegen);
        Instantiate(ItemBox, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        startTimer = false;
        boxTimer = 0;
        canGrabItem = true;
    }
}
