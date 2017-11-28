using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCubeSc : MonoBehaviour {

    public float xRot;
    public float yRot;
    public float zRot;
    public GameObject QMark;

    // Use this for initialization
    void Start () {
        Instantiate(QMark, gameObject.transform.position, Quaternion.identity, gameObject.transform);//It might be better to have the itembox instantiate this, or have the itembox be the parent and set world rotation as needed -Clair
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
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
          other.GetComponent<PlayerMovement>().StartCoroutine(other.GetComponent<PlayerMovement>().Flip());
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(xRot, yRot, zRot);
    }
}
