using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rythm_Manager.Dhit = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rythm_Manager.Dhit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rythm_Manager.Dhit = false;
    }
}
