using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class Cutscene : MonoBehaviour {
    public List<Sprite> cutscenes = new List<Sprite>();
    public static Image panel;
    public static Cutscene instance;
	void Start () {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);

        for(int i=0;i<cutscenes.Count; i++)
        {
            cutscenes[i] = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Scenes/Ch1-" + (i + 1) + ".png", typeof(Sprite));
        }

	}
	
	// Update is called once per frame
	void Update () { 
	}
}
