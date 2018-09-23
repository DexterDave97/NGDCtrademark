using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DavidFall : MonoBehaviour {

    [SerializeField] Animator fadePanel;
    public float var;
    Rigidbody2D play;
    public bool trig;

    private void Start()
    {
        play = GetComponent<Rigidbody2D>();
        if (trig)
            play.velocity = Vector3.down * var;
        else
        {
            play.velocity = Vector3.left * var;
            StartCoroutine(Hello());
        }
    }
    public void changeScene()
    {
        TriggerNextScene.zoomCam = true;
    }    

    IEnumerator Hello()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
