using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrans : MonoBehaviour {
    public Animator camAnim,fade1,fade2;
    public Camera mainCam;
    [SerializeField] float finalCamPos;
    public bool triggered = false;
    public static bool ended;
   
    // Use this for initialization
    void Start () {
        ended = false;
        mainCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if(triggered && !ended)
            moveCam();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            triggered = true;
        }

        fade1.SetBool("inEle", true);
        fade2.SetBool("inEle", true);
    }

    public void moveCam()
    {
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, new Vector3(finalCamPos,mainCam.transform.position.y, mainCam.transform.position.z), 0.05f);
        mainCam.orthographicSize = Vector3.Lerp(Vector3.right * mainCam.orthographicSize, Vector3.right * 6.1f, 0.05f).x;
        if(mainCam.orthographicSize<=6.15f)
        {
            ended = true;
        }
    }
}
