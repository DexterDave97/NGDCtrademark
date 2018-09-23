using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeacherDialogue : MonoBehaviour {

    public string[] dailogues;
    public Text tx;
    int index = 0;
    float timer = 0;
    bool trig = true;

    private void Update()
    {
        if(index <= dailogues.Length)
            Trig();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0;
        }
    }

    private void Trig()
    {
        RaycastHit2D col = Physics2D.Raycast(transform.position, Vector3.up);

        if (col.collider.tag == "Player")
        {
            Debug.Log("Hmm");
            if(trig && timer == 0)
            {
                PlayerController.canmove = false;
                if (index == dailogues.Length)
                {
                    tx.text = "";
                    PlayerController.canmove = true;
                }
                else
                {
                    StartCoroutine(RollText.rollText(dailogues[index], tx));
                    trig = false;
                }
            }
            if(RollText.ended)
            {
                index++;
                trig = true;
                RollText.ended = false;
                timer = 2;
            }
        }
    }


}
