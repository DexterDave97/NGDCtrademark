using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject player;
    public float camXPosMax, camXPosMin;
    public static CameraFollow camfol;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        camfol = this;
    }
	
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
