using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseJumpFall : MonoBehaviour {

    private Rigidbody2D temp;
    private bool trig = false;

    private void Awake()
    {
        temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trig = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trig = false;
        }
    }

    private void Update()
    {
        if(trig)
        {
            trig = false;
            temp.velocity = new Vector2(temp.velocity.x / 1.5f, temp.velocity.y);
        }
    }
}
