using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {
    public GameObject nextStair;
	// Use this for initialization
	void Start () {
        nextStair.GetComponent<Collider2D>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
            nextStair.GetComponent<Collider2D>().enabled = true;
    }
}