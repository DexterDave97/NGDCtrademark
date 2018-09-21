using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseJumpFall : MonoBehaviour
{
    GameObject cutsceneManager;
    private Rigidbody2D temp;
    bool trig = false;

    private void Awake()
    {
        cutsceneManager = GameObject.FindGameObjectWithTag("Cutscene");
        temp = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trig = true;
        }
    }

    private void Update()
    {
        if (trig)
        {
            trig = false;
            cutsceneManager.SetActive(true);
            Cutscene.cutsceneIndex = 5;
            Cutscene.playCutscene = true;
        }
    }
}
