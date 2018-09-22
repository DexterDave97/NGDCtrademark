using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolCam : MonoBehaviour {
    [SerializeField] float camXPosMax, camXPosMin, camYPosMax;
    [SerializeField] GameObject player;
    private float offset;
    ElevatorTrans et;

    // Use this for initialization
    void Start () {
        et = FindObjectOfType<ElevatorTrans>();
        player = GameObject.FindGameObjectWithTag("Player");
        offset = -player.transform.position.y + transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(ElevatorTrans.ended)
        {
            PlayerController.canmove = true;
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
