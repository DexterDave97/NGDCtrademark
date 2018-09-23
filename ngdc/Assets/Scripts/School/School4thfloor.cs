using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class School4thfloor : MonoBehaviour {
    public static int doorCount;
    Animator fadePanel;
    QTSlider qt;
    bool trig;
    int hit = 0, miss = -1;
    [SerializeField] GameObject sliderPrefab;


    private void Start()
    {
        doorCount = 0;
        trig = false;
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            trig = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            trig = false;
    }

    IEnumerator TriggerCutscene()
    {
        fadePanel.SetBool("out", true);
        yield return new WaitForSeconds(1);
        if(doorCount==3)
        {
            Cutscene.cutsceneIndex = 8;
        }
        else
        {
            Cutscene.cutsceneIndex = 6;
        }

        Cutscene.playCutscene = true;
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (qt != null && hit < 3)
        {
            int i = qt.PointerHit();
            if (i == 1)
            {
                hit++;
                Destroy(qt.gameObject);
                if (hit < 3)
                    hitcall();
                if (hit == 3)
                {
                    doorCount++;
                    StartCoroutine(TriggerCutscene());
                }
            }
            else if (i == 0)
            {
                miss++;
            }
        }

        if (trig && Input.GetKeyDown(KeyCode.E))
        {
            trig = false;
            hitcall();
        }
    }

    void hitcall()
    {
        qt = Instantiate(sliderPrefab, FindObjectOfType<Canvas>().transform).GetComponent<QTSlider>();
        PlayerController.canmove = false;
    }
}