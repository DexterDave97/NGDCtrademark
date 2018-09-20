using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour {
    [SerializeField] GameObject items;
    [SerializeField] Popup p;
    [SerializeField] selection s;
    Rigidbody2D rb2d;
    
    
    void Awake () {
        p = GetComponent<Popup>();
        s = GetComponent<selection>();
        rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        items.SetActive(false) ; 
    }
	
	
	void Update () {
        zoom();
    }
    void zoom()
    {
        if (Input.GetKeyDown(KeyCode.E) && items.activeInHierarchy == false && p.button.enabled == true)
        {
            rb2d.velocity = new Vector3(0, 0, 0);
            items.SetActive(true);
            PlayerControl.canmove = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.E) && items.activeInHierarchy == true )
        {
            items.SetActive(false);
            s.tx.text = null;
            PlayerControl.canmove = true;
        }
    }
}
