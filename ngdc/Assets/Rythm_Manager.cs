using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rythm_Manager : MonoBehaviour {

    [SerializeField]
    GameObject W, A, D;

    public static bool Whit, Ahit, Dhit;

    private void Start()
    {
        Whit = false;
        Ahit = false;
        Dhit = false;
    }

    private void Update()
    {
        if (Whit)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
        }

        if (Ahit)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

            }
        }

        if (Dhit)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.D))
            {

            }
        }
    }
}
