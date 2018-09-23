using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{

    public Animator anim;
    public Animator Lift;
    [SerializeField] bool switchable;
    // Use this for initialization
    void Start()
    {
        switchable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && switchable)
        {
            anim.SetBool("click", true);
            Lift.SetBool("CallElevator", true);
            PlayerController.canmove = false;
            Invoke("TempFun", 2);
        }
    }

    void TempFun()
    {
        PlayerController.canmove = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switchable = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            switchable = false;
        }
    }
}
