using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Cutscene : MonoBehaviour
{

    public static Cutscene instance;

    public static int cutsceneIndex = 0, currentSprite = 0;
    public static bool playCutscene;
    Image panel;

    public List<Sprite> cutscene1 = new List<Sprite>();
    public List<Sprite> cutscene2 = new List<Sprite>();
    public List<Sprite> cutscene3 = new List<Sprite>();
    public List<Sprite> cutscene4 = new List<Sprite>();
    public List<Sprite> cutscene5 = new List<Sprite>();

    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<Image>();

        playCutscene = false;

        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);

        for (int temp = 0; temp < 3; temp++)
        {
            cutscene1.Add((Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (temp + 1) + ".png", typeof(Sprite)));
        }
        for (int temp = 0; temp < 5; temp++)
        {
            cutscene2.Add((Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (temp + 4) + ".png", typeof(Sprite)));
        }
        for (int temp = 0; temp < 6; temp++)
        {
            cutscene3.Add((Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (temp + 9) + ".png", typeof(Sprite)));
        }
        for (int temp = 0; temp < 9; temp++)
        {
            cutscene4.Add((Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (temp + 15) + ".png", typeof(Sprite)));
        }
        for (int temp = 0; temp < 2; temp++)
        {
            cutscene5.Add((Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (temp + 24) + ".png", typeof(Sprite)));
        }
    }

    private void Update()
    {
        if (playCutscene)
        {
            Debug.Log(cutsceneIndex);
            
            PlayerController.canmove = false;

            if (Input.GetKeyDown(KeyCode.A))
            {
                currentSprite--;
                currentSprite = Mathf.Clamp(currentSprite, 0, GetCutscene(cutsceneIndex).Count);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentSprite++;
                currentSprite = Mathf.Clamp(currentSprite, 0, GetCutscene(cutsceneIndex).Count);
                if (currentSprite == GetCutscene(cutsceneIndex).Count)
                {
                    currentSprite--;
                    playCutscene = false;
                    if (cutsceneIndex != 4)
                        PlayerController.canmove = true;
                }
            }

            Debug.Log(currentSprite);

            panel.sprite = GetCutscene(cutsceneIndex)[currentSprite];

        }
        else
        {
            currentSprite = 0;
            GameObject.FindGameObjectWithTag("Cutscene").SetActive(false);
        }
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
            default:
                return null;
        }
    }
}
