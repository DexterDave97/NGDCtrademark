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
            PlayerController.lives = 0;
            GameObject.FindGameObjectWithTag("Death2").GetComponent<Animator>().SetBool("out", false);
            GameObject.FindGameObjectWithTag("Death1").GetComponent<Animator>().SetBool("out", false);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}