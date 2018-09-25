using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMove : MonoBehaviour {
    public Animator anim;
    public GameObject cam, player,elevator;
    public bool switchable, play;
    Vector3 playerFol, camFol;
    AudioSource ac;
    // Use this for initialization
    void Start () {
        play = false;
        switchable = false;
        player = GameObject.FindGameObjectWithTag("Player");
        ElevatorIsDown.isDown = false;
        cam = Camera.main.gameObject;
        ac = GameObject.FindGameObjectWithTag("Secondary Audio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (switchable)
        {
            if (play)
            {
                play = false;
                ac.clip = FindObjectOfType<Sounds>().audioDict["Corridor"][2];
                ac.PlayDelayed(0.5f);
            }
            if (ElevatorTrans.ended)
            {
                startToMoveEle();
                player.transform.parent = elevator.transform;
                cam.transform.parent = player.transform;
            }            
        }
        if (ElevatorIsDown.isDown)
        {
            ac.Stop();            
            player.transform.parent = null;
            switchable = false;
        }

    }

    void startToMoveEle()
    {
        float speed = 0.2f;
        if (elevator.transform.position.y < -150)
            speed -= Time.fixedDeltaTime * 7;
        elevator.transform.position = Vector3.MoveTowards(elevator.transform.position, new Vector3(elevator.transform.position.x, -161.5f, elevator.transform.position.z), speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {            
            switchable = true;
            play = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
