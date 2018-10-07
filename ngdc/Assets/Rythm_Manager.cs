using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rythm_Manager : MonoBehaviour {

    public float offset = 20f;
    [SerializeField]
    GameObject W, A, D;
    [SerializeField]
    GameObject C1, C2, C3;

    Vector3 Worg, Aorg, Dorg;

    bool Whit, Ahit, Dhit;
    bool lockW = false, lockA = false, lockD = false;

    float timeW, timeA, timeD;

    private void Awake()
    {
        Worg = W.transform.localPosition;
        Aorg = A.transform.localPosition;
        Dorg = D.transform.localPosition;
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
        Debug.Log(Whit + " " + Ahit + " " + Dhit);
        MoveImage();
        CheckHit();
        CheckInput();
    }

    void MoveImage()
    {
        if (timeW + 3.3 < Time.time)
        {
            timeW = Time.time;
            W.SetActive(true);
            A.SetActive(true);
            D.SetActive(true);
            lockA = lockD = lockW = false;
            W.transform.localPosition = Worg;
            D.transform.localPosition = Dorg;
            A.transform.localPosition = Aorg;
        }
        
        W.transform.localPosition -= new Vector3(250 * Time.deltaTime, 0, 0);
        D.transform.localPosition -= new Vector3(250 * Time.deltaTime, 0, 0);
        A.transform.localPosition -= new Vector3(250 * Time.deltaTime, 0, 0);
    }

    void CheckHit()
    {
        if (C1.transform.localPosition.x + offset > W.transform.localPosition.x && C1.transform.localPosition.x - offset < W.transform.localPosition.x)
            Whit = true;
        else Whit = false;
        if (C2.transform.localPosition.x + offset > A.transform.localPosition.x && C2.transform.localPosition.x - offset < A.transform.localPosition.x)
            Ahit = true;
        else Ahit = false;
        if (C3.transform.localPosition.x + offset > D.transform.localPosition.x && C3.transform.localPosition.x - offset < D.transform.localPosition.x)
            Dhit = true;
        else Dhit = false;
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
                C1.GetComponent<Animator>().SetBool("S", false);
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
                C2.GetComponent<Animator>().SetBool("S", false);
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
                C3.GetComponent<Animator>().SetBool("S", false);
            }
        }
    }
}
