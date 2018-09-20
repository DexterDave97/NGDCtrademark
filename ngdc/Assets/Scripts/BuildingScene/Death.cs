using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject respawn;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("YOU DIED !!!");
            player.transform.position = respawn.transform.position + new Vector3(0, player.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            PlayerController.Dir = 0;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}
