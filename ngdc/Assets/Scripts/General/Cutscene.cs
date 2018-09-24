using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public static Cutscene instance;
    public static int cutsceneIndex, currentSprite = 0;
    public static bool playCutscene, sceneEnd;
    public static string nextSceneName;
    private float timeLol;
    GameObject cutsceneGameobject;
    Image panel;
    Animator fadePanel;
    int maxScene;
    bool timebool;

    public List<Sprite> cutscene1 = new List<Sprite>();
    public List<Sprite> cutscene2 = new List<Sprite>();
    public List<Sprite> cutscene3 = new List<Sprite>();
    public List<Sprite> cutscene4 = new List<Sprite>();
    public List<Sprite> cutscene5 = new List<Sprite>();
    public List<Sprite> cutscene6 = new List<Sprite>();
    public List<Sprite> cutscene7 = new List<Sprite>();
    public List<Sprite> cutscene8 = new List<Sprite>();

    void Start()
    {
        timebool = true;
        cutsceneIndex = 0;
        maxScene = 0;
        fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        panel = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Image>();
        cutsceneGameobject = panel.gameObject;
        sceneEnd = false;
        if (SceneManager.GetActiveScene().name == "House" && LevelTransition.playerPos == null)
        {
            playCutscene = true;
            cutsceneIndex = 1;
        }
        else
            playCutscene = false;

        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
        /*
        for (int temp = 0; temp < 3; temp++)
        {
            cutscene1.Add(Resources.Load<Sprite>("Ch1-" + (temp + 1) + ".png" ));
        }
        for (int temp = 0; temp < 5; temp++)
        {
            cutscene2.Add(Resources.Load<Sprite>("Ch1-" + (temp + 4) + ".png" ));
        }
        for (int temp = 0; temp < 6; temp++)
        {
            cutscene3.Add(Resources.Load<Sprite>("Ch1-" + (temp + 9) + ".png" ));
        }
        for (int temp = 0; temp < 9; temp++)
        {
            cutscene4.Add(Resources.Load<Sprite>("Ch1-" + (temp + 15) + ".png" ));
        }
        for (int temp = 0; temp < 2; temp++)
        {
            cutscene5.Add(Resources.Load<Sprite>("Ch1-" + (temp + 24) + ".png" ));
        }
        for (int temp = 0; temp < 2; temp++)
        {
            cutscene6.Add(Resources.Load<Sprite>("Ch1-" + (temp + 90) + ".png" ));
        }
        for (int temp = 0; temp < 11; temp++)
        {
            cutscene7.Add(Resources.Load<Sprite>("Ch1-" + (temp + 26) + ".png" ));
        }
        for (int temp = 0; temp < 8; temp++)
        {
            cutscene8.Add(Resources.Load<Sprite>("Ch1-" + (temp + 37) + ".png" ));
        }*/
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad <= Time.fixedDeltaTime && SceneManager.GetActiveScene().name != "Bedroom" && SceneManager.GetActiveScene().name != "SchoolBedroom" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
            if(SceneManager.GetActiveScene().name != "FallingBuildingScene" && SceneManager.GetActiveScene().name != "BuildingEnding")
            {
                panel = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Image>();
                cutsceneGameobject = panel.gameObject;
            }
        }

        if (playCutscene)
        {
            if(timebool)
            {
                timeLol = Time.time;
                timebool = false;
            }

            panel.enabled = true;

            PlayerController.canmove = false;

            if (Input.GetKeyUp(KeyCode.A))
            {
                currentSprite--;
                currentSprite = Mathf.Clamp(currentSprite, 0, GetCutscene(cutsceneIndex).Count);
            }
            if (Input.GetKeyUp(KeyCode.D) && currentSprite < maxScene)
            {
                timeLol = Time.time;
                timebool = true;
                currentSprite++;
                currentSprite = Mathf.Clamp(currentSprite, 0, GetCutscene(cutsceneIndex).Count);
                if (currentSprite == GetCutscene(cutsceneIndex).Count)
                {
                    currentSprite--;
                    playCutscene = false;
                    StartCoroutine(TriggerCut());
                    if (sceneEnd)
                    {
                        SceneManager.LoadScene(nextSceneName);
                        sceneEnd = false;
                    }
                    if(cutsceneIndex == 3)
                    {
                        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(26, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
                        Camera.main.transform.position = new Vector3(32.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                        CameraFollow.camfol.camXPosMin = 32.5f;
                        CameraFollow.camfol.camXPosMax = 42.5f;
                    }
                    //if (cutsceneIndex != 4)
                    PlayerController.canmove = true;
                }
            }
            else if(Input.GetKeyUp(KeyCode.D) && currentSprite >= maxScene && ((Time.time - timeLol) > 1.2f))
            {
                timeLol = Time.time;
                timebool = true;
                currentSprite++;
                maxScene = currentSprite;
                currentSprite = Mathf.Clamp(currentSprite, 0, GetCutscene(cutsceneIndex).Count);
                if (currentSprite == GetCutscene(cutsceneIndex).Count)
                {
                    currentSprite--;
                    playCutscene = false;
                    StartCoroutine(TriggerCut());
                    if (sceneEnd)
                    {
                        SceneManager.LoadScene(nextSceneName);
                        sceneEnd = false;
                    }
                    if (cutsceneIndex == 3)
                    {
                        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(26, GameObject.FindGameObjectWithTag("Player").transform.position.y, GameObject.FindGameObjectWithTag("Player").transform.position.z);
                        Camera.main.transform.position = new Vector3(32.5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
                        CameraFollow.camfol.camXPosMin = 32.5f;
                        CameraFollow.camfol.camXPosMax = 42.5f;
                    }
                    //if (cutsceneIndex != 4)
                    PlayerController.canmove = true;
                }
            }

            panel.sprite = GetCutscene(cutsceneIndex)[currentSprite];
            Color tCol;
            tCol = panel.color;
            tCol.a = 1;
            panel.color = tCol;

        }
        else
        {
            currentSprite = 0;
            maxScene = 0;
            if (cutsceneGameobject == true)
                panel.enabled = false;
        }
    }

    IEnumerator TriggerCut()
    {
        fadePanel.SetBool("out", false);
        Color tCol2;
        tCol2 = panel.color;
        tCol2.a = 0;
        panel.color = tCol2;
        if (sceneEnd)
        {
            SceneManager.LoadScene(nextSceneName);
            sceneEnd = false;
        }
        if (cutsceneIndex == 7)
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (cutsceneIndex == 8)
        {
            SceneManager.LoadScene("BuildingEnding");
        }
        yield return new WaitForSeconds(0.5f);
    }

    List<Sprite> GetCutscene(int x)
    {
        switch (x)
        {
            case 1:
                return cutscene1;
            case 2:
                return cutscene2;
            case 3:
                return cutscene3;
            case 4:
                return cutscene4;
            case 5:
                return cutscene5;
            case 6:
                return cutscene6;
            case 7:
                return cutscene7;
            case 8:
                return cutscene8;
            default:
                return null;
        }
    }
}
