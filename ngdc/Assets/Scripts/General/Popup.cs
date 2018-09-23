using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Popup : MonoBehaviour {
    [SerializeField] public Image button;
    [SerializeField] public GameObject player;
    [SerializeField] float offset = 3f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        button.enabled = false;
        if (GetComponent<Zoom>() != null)
        {
            GetComponent<Zoom>().enabled = false;
        }
       
    }
    void Update()
    {
        Vector3 buttonPos = new Vector3(player.transform.position.x, player.transform.position.y + offset,0);
        button.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(buttonPos);
    }
   

    void OnTriggerEnter2D(Collider2D col)
    {
        button.enabled = true;
        if (GetComponent<Zoom>()!=null)
        {
            GetComponent<Zoom>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        button.enabled = false;
        if (GetComponent<Zoom>() != null)
        {
            GetComponent<Zoom>().enabled = false;
        }
    }

    
}
