using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    float move, temp;
    [SerializeField] float moveSpeed;
    int x = 0;
    public bool dead;
    Vector3 hew;

    private void Start()
    {
        hew = transform.position;
        temp = hew.x;
    }

    private void Update()
    {
        move = PlayerController.move;

        if (dead)
        {
            hew.x = temp;
            transform.position = hew; x++;
            dead = false;
        }

        if (move != 0)
        {
            if(move == 1 && PlayerController.canmove)
                hew.x -= moveSpeed * 0.01f;
            if(move == -1 && PlayerController.canmove)
                hew.x += moveSpeed * 0.01f;

            transform.position = hew;
        }
    }
}
