using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liveReset : MonoBehaviour {
    [SerializeField]
    Animator d1, d2, d3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        d1.SetBool("out", false);
        d2.SetBool("out", false);
        d3.SetBool("out", false);
        PlayerController.lives = -1;
    }
}
