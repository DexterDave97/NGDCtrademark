﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolCam : MonoBehaviour {
    [SerializeField] float camXPosMax, camXPosMin, camYPosMax;
    [SerializeField] GameObject player;
    private float offset;
    ChangeToFireman et;
    bool canMoveChanger = false;

    // Use this for initialization
    void Start () {
        et = FindObjectOfType<ChangeToFireman>();
        player = GameObject.FindGameObjectWithTag("Player");
        offset = -player.transform.position.y + transform.position.y + 2.1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(ChangeToFireman.ended)
        {
            if (!canMoveChanger)
            {
                PlayerController.canmove = true;
                canMoveChanger = true;
            }
            CamFollow();
        }
        else if (et.triggered)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            PlayerController.canmove = false;
        }
	}

    void CamFollow()
    {
        if(player.transform.position.x <= camXPosMax && player.transform.position.x >= camXPosMin)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
        else if(player.transform.position.x < camXPosMin && player.transform.position.y + offset < camYPosMax)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + offset, transform.position.z);
        }
    }
}