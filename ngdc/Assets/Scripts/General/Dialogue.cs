using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public string[] dailogues;
    public RollText r;
    public Text tx;
    int index = 0;
    float timer = 0;
    bool trig = true, start = false;
    public bool willStopPlayer = false;
    public GameObject[] kuchbhi;

    void Start()
    {
        r = GetComponent<RollText>();
    }

    private void Update()
    {
        if (index <= dailogues.Length)
            Trig();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
        }
    }

    private void Trig()
    { 
        RaycastHit2D col = Physics2D.Raycast(transform.position, -Vector3.up);
        if (col.collider.tag == "Player" && !start)
        {            
            start = true;
            for (int i = 0; i < kuchbhi.Length; i++)
                Destroy(kuchbhi[i]);
            tx.text = "";
        }

        if (start)
        {
            if(trig && timer == 0)
            {
                if(willStopPlayer)
                {
                    PlayerController.canmove = false;
                    PlayerController.jumpingAvailable = false;
                    PlayerController.jumpoverride = false;
                }
                if (index == dailogues.Length)
                {
                    index++;
                    tx.text = "";
                    if (willStopPlayer)
                    {
                        PlayerController.canmove = true;
                        PlayerController.jumpingAvailable = true;
                        PlayerController.jumpoverride = true;
                    }
                }
                else
                {
                    tx.text = "";
                    StartCoroutine(r.rollText(dailogues[index], tx));
                    trig = false;
                }
            }
            if(r.ended)
            {
                index++;
                trig = true;
                r.ended = false;
                timer = 2;
            }
        }
    }


}
