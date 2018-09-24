using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField] Image pop;
    public Animator anim;
    public Animator Lift;
    [SerializeField] bool switchable;
    void Start()
    {
        switchable = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && switchable)
        {
            pop.enabled = false;
            anim.SetBool("click", true);
            if(Cutscene.cutsceneIndex < 3)
                Lift.SetBool("CallElevator", true);
            PlayerController.canmove = false;
            Invoke("TempFun", 1.2f);
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
