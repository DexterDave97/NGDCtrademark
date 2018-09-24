using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    float move, temp;
    [SerializeField] float moveSpeed;
    public bool dead;
    public static float xval;
    Rigidbody2D player;
    Vector3 hew;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        hew = transform.position;
        temp = hew.x;
    }

    private void Update()
    {
        move = player.velocity.x;

        if (dead && PlayerController.isMoving)
        {
            hew.x = temp;
            transform.position = hew;
            dead = false;
        }

        if (move != 0 && PlayerController.isMoving)
        {
            if(move > 0 && PlayerController.canmove)
                hew.x -= moveSpeed * 0.01f;
            if(move < 0 && PlayerController.canmove)
                hew.x += moveSpeed * 0.01f;

            transform.position = hew;
        }
    }
}
