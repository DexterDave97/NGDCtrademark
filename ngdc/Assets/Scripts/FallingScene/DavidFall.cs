using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DavidFall : MonoBehaviour {

    [SerializeField] Animator fadePanel;
    public float var;
    Rigidbody2D play;

    private void Start()
    {
        play = GetComponent<Rigidbody2D>();
        play.velocity = Vector3.down * var;
    }
    public void changeScene()
    {
        TriggerNextScene.zoomCam = true;
    }
}
