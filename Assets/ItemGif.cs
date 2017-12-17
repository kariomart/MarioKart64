using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGif : MonoBehaviour {

	public Image image;
	public Sprite[] spriteList = new Sprite[13];
	public int counter = 0;
	float interval = 0.1f;
	float timer;

	// Use this for initialization
	void Start () {

		image = GetComponent<Image> ();


	}
	
	// Update is called once per frame
	void Update () {


		timer -= Time.deltaTime;

		if (timer < 0) {
			counter++;
			timer = interval;
		}
		image.sprite = spriteList [counter % spriteList.Length];
	}
}
