using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rythm_Manager.Ahit = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rythm_Manager.Ahit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rythm_Manager.Ahit = false;
    }
}
