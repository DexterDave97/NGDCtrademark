using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicided : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject respawn, beti;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.shouldSuicideBool = false;
            Debug.Log("Cutscene Here !!!");

            //
            
            player.transform.position = respawn.transform.position + new Vector3(0, player.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            PlayerController.Dir = 0;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            beti.SetActive(true);
            PlayerController.canmove = false;
            PlayerController.lockSuicide = false;
            PlayerController.jumpingAvailable = false;
        }
    }
}
