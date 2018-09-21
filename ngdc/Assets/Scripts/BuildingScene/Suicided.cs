using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicided : MonoBehaviour
{
    GameObject player;
    bool triggered = false;
    [SerializeField] GameObject respawn, beti;
    [SerializeField] Animator fadePanel;
    GameObject cutsceneManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cutsceneManager = GameObject.FindGameObjectWithTag("Cutscene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(cutsceneTrigger());
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
        cutsceneManager.SetActive(true);
        Cutscene.cutsceneIndex = 4;
        Cutscene.playCutscene = true;
        triggered = true;
    }

    private void Update()
    {
        Debug.Log(fadePanel.GetBool("out"));
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
