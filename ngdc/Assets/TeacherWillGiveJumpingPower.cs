using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherWillGiveJumpingPower : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.jumpingAvailable = true;
    }
}
