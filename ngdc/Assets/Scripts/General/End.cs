using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour {

    GameObject player;
    bool triggered = false;
    Animator fadePanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.shouldSuicideBool = true;
        }
    }

    IEnumerator cutsceneTrigger()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        PlayerController.shouldSuicideBool = false;
        PlayerController.Dir = 0;
        triggered = true;
    }

    private void Update()
    {
        if (PlayerController.shouldSuicideBool == true)
        {
            if (PlayerController.lockSuicide == false)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                PlayerController.canmove = false;
            }
            player.GetComponent<SpriteRenderer>().flipX = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerController.lockSuicide = true;
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(5.5f, 5), ForceMode2D.Impulse);
                StartCoroutine(cutsceneTrigger());
            }
        }

        if (triggered)
        {
            if (Cutscene.playCutscene == false)
            {
                triggered = false;
                PlayerController.canmove = false;
                PlayerController.lockSuicide = false;
                PlayerController.jumpingAvailable = false;
            }
        }
    }
}
