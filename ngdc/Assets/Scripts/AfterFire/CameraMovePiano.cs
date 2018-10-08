using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMovePiano : MonoBehaviour {

    [SerializeField] GameObject upscene;
    [SerializeField] GameObject Blackie;
    [SerializeField] GameObject Violin;
    public static bool startMoving = false;
    bool locked2 = true, locked = true;
    float time;


    public string[] dailogues;
    public RollText r;
    public Text tx;
    int index = 0;
    float timer = 0;
    bool trig = true, start = false;
    public bool willStopPlayer = false;
    public GameObject[] kuchbhi;

    private void Start()
    {
        r = GetComponent<RollText>();
    }

    private void Update()
    {
        if (startMoving)
            Move();

        if (!locked && Time.time > time)
        {
            SceneManager.LoadScene("Credits 1");
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
        }

        DisplayText();
    }

    void Move()
    {
        if (upscene.transform.position.y < 20)
            upscene.transform.position = upscene.transform.position + new Vector3(0, 10f * Time.deltaTime, 0);
        Blackie.transform.position = Blackie.transform.position + new Vector3(0, 6f * Time.deltaTime, 0);
        if (transform.position.y < 15.5f && locked2)
            transform.position = transform.position + new Vector3(0, 5f * Time.deltaTime, 0);
        if (upscene.transform.position.y > 20)
            upscene.transform.position = new Vector3(upscene.transform.position.x, 19.9f, upscene.transform.position.z);

        if (transform.position.y >= -15.5 && upscene.transform.position.y == 19.9f)
        {
            locked2 = false;
            transform.position = transform.position - new Vector3(0, 5f * Time.deltaTime, 0);
        }
        else if(!locked2 && transform.position.y <= -15.5 && upscene.transform.position.y == 19.9f)
        {
            Blackie.SetActive(false);
            Violin.SetActive(false);
            startMoving = false;
            if(locked)
            {
                locked = false;
                time = Time.time + 7;
            }
            start = true;
        }
    }

    void DisplayText()
    {
        if (start)
        {
            if (trig && timer == 0)
            {
                if (willStopPlayer)
                {
                    PlayerController.canmove = false;
                    PlayerController.jumpingAvailable = false;
                    PlayerController.jumpoverride = false;
                }
                if (index == dailogues.Length)
                {
                    index++;
                    tx.text = "";
                    if (willStopPlayer)
                    {
                        PlayerController.canmove = true;
                        PlayerController.jumpingAvailable = true;
                        PlayerController.jumpoverride = true;
                    }
                }
                else
                {
                    tx.text = "";
                    StartCoroutine(r.rollText(dailogues[index], tx));
                    trig = false;
                }
            }
            if (r.ended)
            {
                index++;
                trig = true;
                r.ended = false;
                timer = 2;
            }
        }
    }
}
