using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorIsDown : MonoBehaviour {
    public LayerMask l;
    public static bool isDown;
    bool play;
    [SerializeField] GameObject elevatorWallCollider;
    void Start()
    {
        play = false;
        isDown = false;
    }

    void FixedUpdate()
    {
        if (play)
        {
            play = false;
            GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<Sounds>().audioDict["Corridor"][0]);
        }
        else if(!isDown)
            isHitting();
        
    }
    void isHitting()
    {
        RaycastHit2D rh2D = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y - 4.3f),l);       
        if (rh2D && !play)
        {
            play = true;
            isDown = true;            
            PlayerController.canmove = true;
            Destroy(elevatorWallCollider);
        }
    }
}
