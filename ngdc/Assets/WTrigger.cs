using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rythm_Manager.Whit = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rythm_Manager.Whit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rythm_Manager.Whit = false;
    }
}
