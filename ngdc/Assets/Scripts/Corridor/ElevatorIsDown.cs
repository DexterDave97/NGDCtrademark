using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorIsDown : MonoBehaviour {
    public LayerMask l;
    public static bool isDown;
    [SerializeField] GameObject elevatorWallCollider;
    void Start()
    {
        isDown = false;
    }

    void FixedUpdate()
    {
        isHitting();
    }
    void isHitting()
    {
        RaycastHit2D rh2D = Physics2D.Linecast(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y - 4.3f),l);
        Debug.DrawLine(this.transform.position, new Vector2(this.transform.position.x, this.transform.position.y - 4.3f),Color.white);
        if (rh2D)
        {            
            isDown = true;
            Destroy(elevatorWallCollider);
        }
    }
}
