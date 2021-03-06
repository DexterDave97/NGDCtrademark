﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rythm_Manager : MonoBehaviour
{

    [SerializeField]
    GameObject W, A, D;
    [SerializeField]
    GameObject C1, C2, C3;
    [SerializeField]
    Animator david;

    int[] a1 = { 0,0,0,1,0,1,0,0,0,1,1,0,0,0,0,1,1,0,0,0 }, a2 = { 1,0,0,0,1,0,0,1,1,0,0,0,1,0,0,0,0,1,0,1 }, a3 = { 0,1,1,0,0,0,1,0,0,0,0,1,0,1,1,0,0,0,1,0 };
    int index = -1;
    Color zero, one;

    Vector3 Worg, Aorg, Dorg;

    bool Whit, Ahit, Dhit, play;
    bool lockW = false, lockA = false, lockD = false, playbool = false;

    float timeW;

    int musicindex;

    private void Awake()
    {
        Worg = W.transform.localPosition;
        Aorg = A.transform.localPosition;
        Dorg = D.transform.localPosition;
        W.SetActive(false);
        A.SetActive(false);
        D.SetActive(false);
        zero.a = 0; one.a = 1;
    }

    private void Start()
    {
        Whit = false;
        Ahit = false;
        Dhit = false;
        timeW = Time.time;
        C1.GetComponent<Animator>().SetBool("Idle", true);
        C2.GetComponent<Animator>().SetBool("Idle", true);
        C3.GetComponent<Animator>().SetBool("Idle", true);

        musicindex = -1;
    }

    private void Update()
    {
        //Debug.Log((C1.transform.position.x + C1.GetComponent<RectTransform>().rect.width) + ","+ (C1.transform.position.x - C1.GetComponent<RectTransform>().rect.width));
        if (index < 21)
        {
            MoveImage();
            CheckHit();
            CheckInput();
            if(play && playbool)
            {
                playbool = false;
                GameObject.FindGameObjectWithTag("Secondary Audio").GetComponent<AudioSource>().PlayOneShot(GameObject.FindGameObjectWithTag("Primary Audio").GetComponent<Sounds>().audioDict["Piano"][musicindex]);
            }
        }
    }

    void MoveImage()
    {
        if (timeW + 1.6 < Time.time)
        {
            timeW = Time.time;
            index++;
            W.SetActive(true);
            A.SetActive(true);
            D.SetActive(true);
            musicindex++;
            playbool = true;
            play = false;
            C1.GetComponent<Animator>().SetBool("Idle", true);
            C2.GetComponent<Animator>().SetBool("Idle", true);
            C3.GetComponent<Animator>().SetBool("Idle", true);
            david.SetBool("Playing", false);
            if (index > 19)
            {
                W.SetActive(false);
                A.SetActive(false);
                D.SetActive(false);
                C1.SetActive(false);
                C2.SetActive(false);
                C3.SetActive(false);
                CameraMovePiano.startMoving = true;
            }
            lockA = lockD = lockW = true;
            W.transform.localPosition = Worg;
            D.transform.localPosition = Dorg;
            A.transform.localPosition = Aorg;
        }

        if (index != -1 && index <= 20)
        {
            if (a1[index] == 0)
                W.GetComponent<Image>().color = zero;
            else W.GetComponent<Image>().color = one;
            if (a2[index] == 0)
                A.GetComponent<Image>().color = zero;
            else A.GetComponent<Image>().color = one;
            if (a3[index] == 0)
                D.GetComponent<Image>().color = zero;
            else D.GetComponent<Image>().color = one;
        }

        W.transform.localPosition -= new Vector3(350 * Time.deltaTime, 0, 0);
        D.transform.localPosition -= new Vector3(350 * Time.deltaTime, 0, 0);
        A.transform.localPosition -= new Vector3(350 * Time.deltaTime, 0, 0);
    }

    void CheckHit()
    {
        if (C1.transform.localPosition.x + C1.GetComponent<RectTransform>().rect.width / 2 > W.transform.localPosition.x && C1.transform.localPosition.x - C1.GetComponent<RectTransform>().rect.width / 2 < W.transform.localPosition.x)
            Whit = true;
        else Whit = false;
        if (C2.transform.localPosition.x + C2.GetComponent<RectTransform>().rect.width / 2 > A.transform.localPosition.x && C2.transform.localPosition.x - C2.GetComponent<RectTransform>().rect.width / 2 < A.transform.localPosition.x)
            Ahit = true;
        else Ahit = false;
        if (C3.transform.localPosition.x + C3.GetComponent<RectTransform>().rect.width / 2 > D.transform.localPosition.x && C3.transform.localPosition.x - C3.GetComponent<RectTransform>().rect.width / 2 < D.transform.localPosition.x)
            Dhit = true;
        else Dhit = false;
    }

    void CheckInput()
    {
        if (Whit)
        {
            if (Input.GetKeyDown(KeyCode.W) && lockW && a1[index] == 1)
            {
                lockW = false;
                C1.GetComponent<Animator>().SetBool("S", true);
                C1.GetComponent<Animator>().SetBool("Idle", false);
                play = true;
                david.SetBool("Playing", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && lockW)
            {
                lockW = false;
                C1.GetComponent<Animator>().SetBool("S", false);
                C1.GetComponent<Animator>().SetBool("Idle", false);
                play = false;
                david.SetBool("Playing", false);
            }
        }

        if (Ahit)
        {
            if (Input.GetKeyDown(KeyCode.A) && lockA && a2[index] == 1)
            {
                lockA = false;
                C2.GetComponent<Animator>().SetBool("S", true);
                C2.GetComponent<Animator>().SetBool("Idle", false);
                play = true;
                david.SetBool("Playing", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) && lockA)
            {
                lockA = false;
                C2.GetComponent<Animator>().SetBool("S", false);
                C2.GetComponent<Animator>().SetBool("Idle", false);
                play = false;
                david.SetBool("Playing", false);
            }
        }

        if (Dhit)
        {
            if (Input.GetKeyDown(KeyCode.D) && lockD && a3[index] == 1)
            {
                lockD = false;
                C3.GetComponent<Animator>().SetBool("S", true);
                C3.GetComponent<Animator>().SetBool("Idle", false);
                play = true;
                david.SetBool("Playing", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D) && lockD)
            {
                lockD = false;
                C3.GetComponent<Animator>().SetBool("S", false);
                C3.GetComponent<Animator>().SetBool("Idle", false);
                play = false;
                david.SetBool("Playing", false);
            }
        }
    }
}
