using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCloseTrigger : MonoBehaviour {
    
    [SerializeField] GameObject elevatorCollider;
    [SerializeField] Animator elevator;
    [SerializeField] GameObject Black;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        elevator.SetBool("CallElevator", false);
        elevatorCollider.SetActive(true);
        Black.SetActive(false);
        PlayerPrefs.SetFloat("HouseAfterFire", 35);
    }
}
