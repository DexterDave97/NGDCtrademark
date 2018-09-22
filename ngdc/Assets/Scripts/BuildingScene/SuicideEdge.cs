using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideEdge : MonoBehaviour {

    GameObject player;
    bool triggered = false;
    [SerializeField] GameObject respawn, beti;
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
        player.transform.position = respawn.transform.position + new Vector3(0, player.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
        PlayerController.Dir = 0;
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Cutscene.cutsceneIndex = 4;
        Cutscene.playCutscene = true;
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
                beti.SetActive(true);
                PlayerController.canmove = false;
                PlayerController.lockSuicide = false;
                PlayerController.jumpingAvailable = false;
            }
        }
    }
}
