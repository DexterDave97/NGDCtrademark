using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetWAD : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "W")
            Rythm_Manager.lockW = false;
        if (collision.tag == "A")
            Rythm_Manager.lockA = false;
        if (collision.tag == "D")
            Rythm_Manager.lockD = false;
    }
}
