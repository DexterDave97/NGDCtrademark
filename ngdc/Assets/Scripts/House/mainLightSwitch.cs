using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainLightSwitch : MonoBehaviour {
    public GameObject gb;
    [SerializeField] bool switchable;
	// Use this for initialization
	void Start () {
        switchable = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && switchable)
        {
            if(gb.activeInHierarchy)
            {
                gb.SetActive(false);
            }
            else
            {
                gb.SetActive(true);
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            switchable = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switchable = false;
        }
    }
}