using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaughterDetection : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Daughter")
        {
            PlayerController.canmove = true;
            PlayerController.jumpingAvailable = true;
            PlayerController.lockRun = false;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
