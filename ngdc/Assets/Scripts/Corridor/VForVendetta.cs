using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VForVendetta : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("FireTrigger", false);
            collision.gameObject.GetComponent<Animator>().SetBool("VTrigger", true);
        }
    }
}
