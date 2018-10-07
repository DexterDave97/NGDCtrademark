using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rythm_Manager : MonoBehaviour {

    [SerializeField]
    GameObject W, A, D;
    [SerializeField]
    GameObject C1, C2, C3;

    Vector3 Worg, Aorg, Dorg;

    public static bool Whit, Ahit, Dhit;
    public static bool lockW = false, lockA = false, lockD = false;

    float timeW, timeA, timeD;

    private void Awake()
    {
        Worg = W.transform.position;
        Aorg = A.transform.position;
        Dorg = D.transform.position;
        timeA = timeD = timeW = Time.time;
        W.SetActive(false);
        A.SetActive(false);
        D.SetActive(false);
    }

    private void Start()
    {
        Whit = false;
        Ahit = false;
        Dhit = false;
    }

    private void Update()
    {
        CheckInput();
        MoveImage();
    }

    void MoveImage()
    {
        if (timeW + 3.3 < Time.time)
        {
            timeW = Time.time;
            W.SetActive(true);
            A.SetActive(true);
            D.SetActive(true);
            W.transform.position = Worg;
            A.transform.position = Aorg;
            D.transform.position = Dorg;
        }

        W.transform.position -= new Vector3(20 * Time.deltaTime, 0, 0);
        A.transform.position -= new Vector3(20 * Time.deltaTime, 0, 0);
        D.transform.position -= new Vector3(20 * Time.deltaTime, 0, 0);
    }

    void CheckInput()
    {
        if (Whit)
        {
            if (Input.GetKeyDown(KeyCode.W) && !lockW)
            {
                lockW = true;
                C1.GetComponent<Animator>().SetBool("S", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && !lockW)
            {
                lockW = true;
                C1.GetComponent<Animator>().SetBool("F", true);
            }
        }

        if (Ahit)
        {
            if (Input.GetKeyDown(KeyCode.A) && !lockA)
            {
                lockW = true;
                C2.GetComponent<Animator>().SetBool("S", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) && !lockA)
            {
                lockW = true;
                C2.GetComponent<Animator>().SetBool("F", true);
            }
        }

        if (Dhit)
        {
            if (Input.GetKeyDown(KeyCode.D) && !lockD)
            {
                lockW = true;
                C3.GetComponent<Animator>().SetBool("S", true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D) && !lockD)
            {
                lockW = true;
                C3.GetComponent<Animator>().SetBool("F", true);
            }
        }
    }
}
