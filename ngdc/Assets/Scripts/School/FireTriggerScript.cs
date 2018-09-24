using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTriggerScript : MonoBehaviour {

    [SerializeField] GameObject fire;
    bool trig = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !trig)
        {
            trig = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if(trig)
        {
            fire.GetComponent<Rigidbody2D>().velocity = new Vector3( 0, 200f * Time.deltaTime, 0);
        }
    }
}
