using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject respawn;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.transform.position = respawn.transform.position + new Vector3(0, (respawn.transform.localScale.y / 2) + player.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            PlayerController.Dir = 0;
            Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            Parallax[] arr = new Parallax[2];
            arr = GameObject.FindObjectsOfType<Parallax>();
            for (int i = 0; i < arr.Length; i++)
                arr[i].dead = true;
        }
    }
}