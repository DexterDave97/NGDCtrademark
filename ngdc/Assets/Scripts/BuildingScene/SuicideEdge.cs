using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEdge : MonoBehaviour {

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.shouldSuicideBool = true;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.shouldSuicideBool == true)
        {
            if (PlayerController.lockSuicide == false)
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<SpriteRenderer>().flipX = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerController.lockSuicide = true;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2((15f * Mathf.Cos(1f * Mathf.Deg2Rad)), ((15f * Mathf.Sin(1f * Mathf.Deg2Rad)) - (9.8f * Time.deltaTime)));
            }
        }
    }
}
