using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] float camXPosMax, camXPosMin;
    [SerializeField] GameObject player; // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        CamFollow();
	}

    void CamFollow()
    {
        if (player.transform.position.x <= camXPosMax && player.transform.position.x >= camXPosMin)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
