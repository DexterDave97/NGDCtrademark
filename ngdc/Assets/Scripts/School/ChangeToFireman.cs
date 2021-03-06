﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToFireman : MonoBehaviour {
    public Animator camAnim, fade1, fade2;
    public Camera mainCam;
    [SerializeField] float finalCamPos, finalCamSize;
    public bool triggered = false;
    public static bool ended;

    // Use this for initialization
    void Start()
    {
        ended = false;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered && !ended)
            moveCam();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            triggered = true;
            GameObject.FindGameObjectWithTag("Footstep").GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<Sounds>().audioDict["Player"][2];
            GameObject.FindGameObjectWithTag("Secondary Audio").GetComponent<AudioSource>().clip = GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<Sounds>().audioDict["Corridor"][5];
            GameObject.FindGameObjectWithTag("Secondary Audio").GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<Animator>().SetBool("FireTrigger", true);
            col.gameObject.GetComponent<Animator>().SetBool("VTrigger", false);
        }

        fade1.SetBool("inEle", true);
        fade2.SetBool("inEle", true);
    }

    public void moveCam()
    {
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(finalCamPos, 1.5f, mainCam.transform.position.z), 0.05f);
        mainCam.orthographicSize = Vector3.Lerp(Vector3.right * mainCam.orthographicSize, Vector3.right * finalCamSize, 0.05f).x;
        if (mainCam.orthographicSize <= (finalCamSize + 0.05f))
        {
            ended = true;
        }
    }
}