using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour {
    [SerializeField] GameObject blackPanel, grad;
    [SerializeField] Animator anim, fadePanel;
    [SerializeField] float finalCamPos;
    bool ended;
    public static bool zoomCam;
    Camera mainCam;
    void Start()
    {
        zoomCam = false;
        ended = false;
        mainCam = Camera.main;
        blackPanel.SetActive(false);
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetFloat("SchoolHouse", 11.7f);
            blackPanel.SetActive(true);
            mainCam.orthographicSize = 1f;
            mainCam.transform.localPosition = new Vector3(2.1f, mainCam.transform.localPosition.y, mainCam.transform.localPosition.z);
            Invoke("BedSchool", 2);
        }
    }
    void cameraZoomOnFace()
    {
        mainCam.transform.localPosition = Vector3.Lerp(mainCam.transform.localPosition, new Vector3(-8.5f, 1.51f, mainCam.transform.localPosition.z), 0.01f);
        grad.transform.localPosition = Vector3.Lerp(grad.transform.localPosition, new Vector3(-0.05f, grad.transform.localPosition.y, grad.transform.localPosition.z), 0.01f);
        mainCam.orthographicSize = Vector3.Lerp(Vector3.right * mainCam.orthographicSize, Vector3.right * 11.3f, 0.01f).x;
        if (mainCam.orthographicSize >= 11.2f)
        {
            ended = true;
            StartCoroutine(nextScene());
        }
    }

    void BedSchool()
    {
        anim.SetBool("Sleep", true);
        blackPanel.SetActive(false);
        fadePanel.gameObject.SetActive(false);
        fadePanel.gameObject.SetActive(true);
    }

    IEnumerator nextScene()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("SchoolBedroom");
    }

    void Update()
    {
        if(zoomCam && !ended)
        {
            PlayerController.lives = 0;
            cameraZoomOnFace();
        }
    }
}

